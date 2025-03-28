![image](./ark-dark.png)
# MessagePack.NodaTime

This library adds support for NodaTime types to MessagePack C#.

## Getting Started

### Installation
#### Prerequisities for C#
* [MessagePack](https://www.nuget.org/packages/MessagePack/)
* [NodaTime](https://www.nuget.org/packages/NodaTime/)


This library is provided in NuGet.

Support for .NET Framework 4.5, .NET Framework 4.6.1, .NET Standard 1.6 and .NET Standard 2.0.

In the Package Manager Console -
```
Install-Package MessagePack.NodaTime
```
or download directly from NuGet.
## How to use
To use the NodaTime resolver, you will have to add it to the composite resolver, as shown in the example below:
```csharp
 CompositeResolver.RegisterAndSetAsDefault(
                BuiltinResolver.Instance,
                AttributeFormatterResolver.Instance,
                SourceGeneratedFormatterResolver.Instance,
                NodatimeResolver.Instance,
                DynamicEnumAsStringResolver.Instance,
                ContractlessStandardResolver.Instance
            );
```
## Quick Start
For more information on either MessagePack or NodaTime, please follow the respective links below. 
* [MesssagePack](https://github.com/neuecc/MessagePack-CSharp/blob/master/README.md)
* [NodaTime](https://nodatime.org/)

This is a quick guide on a basic serialization and de-serialization of a NodaTime type.

```csharp
Instant inst = new Instant();
var bin = MessagePackSerializer.Serialize(inst);
var res = MessagePackSerializer.Deserialize<Instant>(bin);
// inst == res
```

## Usage
### Supported NodaTime types
 `Insant`, `LocalTime`,  `LocalDate`,  `LocalDateTime`,`Offset`, `OffsetDateTime`, `Period` and `ZonedDateTime`

### Timestamps
#### Serialization
As per the MessagePack spec, when we serialize a NodaTime type of LocalDateTime, LocalDate or Instant, an extension type of -1 is received meaning it is a MessagePack timestamp.

Timestamp spec can be found [here](https://github.com/msgpack/msgpack/blob/master/spec.md#timestamp-extension-type).

An example of this in C# is shown below:
```csharp
LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
// This date is within the range for timestamp32

var localDateTimeBinary = MessagePackSerializer.Serialize(ldt);
// Once serialized we can expect the format to be [0xd6, -1, data] (format, extension type, data in bytes),
// and ‘localDateTimeBinary’ to be a byte array of size 6
```

#### Deserialization
In the same way we can support serialization from NodaTime (eg, LocalDate) to MessagePack (timestamp), the same is applied for deserialization. 

From a timestamp, we can deserialize into a LocalDate (if time part is 0), LocalDateTime or an Instant.

From the snippet of code in serialization, shown below is deserialization:
```csharp
var res = MessagePackSerializer.Deserialize<LocalDateTime>(localDateTimeBinary);
```
:heavy_exclamation_mark: Deserializing a LocalDateTime into a LocalDate, will not work if the time value is not 0.

### NodaTime serialized formats
<table>
  <tr><th>NodaTime type</th><th>Serialization format</th></tr>
  <tr><td>Instant</td><td>When an Instant is serialized, like LocalDateTime and LocalDate, it goes to timestamp format. Depending on the value of the Instant, it will fall into either timestamp 32, 64, or 96 format, as explained above under the Timestamp heading.</td></tr>
  <tr><td>LocalDate</td><td>Once a LocalDate is serialized it is in timestamp format. Depending on the value of the LocalDate, it will fall into either timestamp 32, 64 or 96. LocalDate has no time values.</td></tr>
  <tr><td>LocalDateTime</td><td>Once a LocalDate is serialized it is in timestamp format. This means an extension type of -1 will be received by MessagePack. LocalDateTime can be deserialized into a LocalDate if it has no time part.</td></tr>
  <tr><td>LocalTime</td><td>LocalTime is serialized into an int64 (64 bit int). The int64 contains the LocalTime value in nanoseconds.</td></tr>
  <tr><td>Offset</td><td>Offset is serialized into an int32 (32 bit int). The int32 contains the Offset value in seconds.</td></tr>
  <tr><td>OffsetDateTime</td><td>When an Offset is serialized, it is split up into into the LocalDateTime and Offset parts.
They are then serialized using there respective formatters. 
This means the serialized OffsetDateTime will be put into an array of 2 elements which looks like [timestamp, int32].
The Offset and LocalDateTime serialization is explained in the headings above.</td></tr>
  <tr><td>Period</td><td>When the NodaTime type Period is serialized, it is split into a 'fixarray'. 
For a Period we have a 10 element array of four int32 amd six int64 respectively, represented in the order of → 
Years, Months, Weeks, Days, Hours, Minutes, Seconds, Milliseconds, Ticks, Nanoseconds.</td></tr>
  <tr><td>ZonedDateTime</td><td>A ZonedDateTime is split up into LocalDateTime, an Offset and a string representing a Zone, during serialization.
This means the ZonedDateTime is put into an array of 3 elements.
Each NodaTime type is serialized using there respective formatters, 
while the string is serialized using the MessagePack base class into a 'fixstr'.</td></tr>
</table>

## Limitations
### Nanoseconds
While NodaTime supports nanoseconds accuracy, we currently do not. The lowest common level of precision between us and NodaTime is ticks. This means our serialization and deserialization process truncates at 100 nanoseconds because 100ns = 1 tick. 
Below are two examples explaining this:
```csharp
LocalDateTime ldt = new LocalDateTime(2016, 08, 21, 0, 0, 0, 0).PlusNanoseconds(1)

var localDateTimeBinary = MessagePackSerializer.Serialize(ldt);
var result MessagePackSerializer.Deserialize<LocalDateTime>(localDateTimeBinary);

// ldt != result, nanosecond accuracy is lost in process.
```

```csharp
LocalDateTime ldt = new LocalDateTime(2016, 08, 21, 0, 0, 0, 0).PlusNanoseconds(100);

var localDateTimeBinary = MessagePackSerializer.Serialize(ldt);
var result = MessagePackSerializer.Deserialize<LocalDateTime>(localDateTimeBinary);

// ldt == result, returns truncated value equal to 1 tick.
```

### UTC
In the base [MessagePack](https://github.com/neuecc/MessagePack-CSharp) library, DateTime values are converted to UTC before being serialized. While using our library, you must specify DateTimeKind as UTC before serializing when using DateTime and the LocalDateTime type, and expect it as UTC when deserializing.

## Interoperability
As explained previously, we use the timestamp format for some of our serialized NodaTime types. The timestamp format is interoperable with [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp), the official [MsgPack library](https://github.com/msgpack/msgpack/blob/master/spec.md) and any other MessagePack implementations that support the extension type of -1.

## Contributing
*TBC*

## Links
* [Nuget](https://www.nuget.org/packages/MessagePack.NodaTime/)
* [Github](https://github.com/ARKlab/MessagePack)
* [Ark Energy](http://www.ark-energy.eu/)


## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/ARKlab/MessagePack/blob/master/LICENSE) file for details

## Acknowledgments
* [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp)
* [NodaTime](https://nodatime.org/)
* [MsgPack Spec](https://github.com/msgpack/msgpack/blob/master/spec.md)
