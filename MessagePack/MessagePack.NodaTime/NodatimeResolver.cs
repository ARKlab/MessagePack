﻿// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using System;
using System.Collections.Generic;

namespace MessagePack.NodaTime
{
    public sealed class NodatimeResolver : IFormatterResolver
    {
        // Resolver should be singleton.
        public static IFormatterResolver Instance = new NodatimeResolver();

        NodatimeResolver()
        {
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static FormatterCache() => formatter = (IMessagePackFormatter<T>)NodatimeResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }

    internal static class NodatimeResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
        {
            {typeof(Instant), InstantMessagePackFormatter.Instance},
            {typeof(LocalDate), LocalDateAsDatetimeMessagePackFormatter.Instance},
            {typeof(LocalTime), LocalTimeAsNanosecondsMessagePackFormatter.Instance},
            {typeof(LocalDateTime), LocalDateTimeAsDateTimeMessagePackFormatter.Instance},

            {typeof(Offset), OffsetMessagePackFormatter.Instance},
            {typeof(Period), PeriodAsIsoStringMessagePackFormatter.Instance},

            {typeof(OffsetDateTime), OffsetDateTimeMessagePackFormatter.Instance},
            {typeof(ZonedDateTime), ZonedDateTimeMessagePackFormatter.Instance},

            {typeof(Instant?), new NullableFormatter<Instant>() },
            {typeof(LocalDate?), new NullableFormatter<LocalDate>() },
            {typeof(LocalTime?), new NullableFormatter<LocalTime>() },
            {typeof(LocalDateTime?), new NullableFormatter<LocalDateTime>() },
            {typeof(Offset?), new NullableFormatter<Offset>() },
            {typeof(OffsetDateTime?), new NullableFormatter<OffsetDateTime>() },
            {typeof(ZonedDateTime?), new NullableFormatter<ZonedDateTime>() },
        };

        internal static object GetFormatter(Type t)
        {
            if (formatterMap.TryGetValue(t, out var formatter))
            {
                return formatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }
    }

}