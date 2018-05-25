![image](http://www.ark-energy.eu/wp-content/uploads/ark-dark.png)
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
Install-Package
```
or download directly from NuGet.

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

As seen in the spec, the first part of a timestamp is the format type. The next part (in timestamp32 and 64) is the extension type as mentioned above. In timestamp96, the second part of it defines the length of the data in the timestamp. The rest of the timestamps are made up of bytes of data from what you serialize.

Timestamp spec can be found [here](https://github.com/msgpack/msgpack/blob/master/spec.md#timestamp-extension-type).

An example of this in C# is shown below:
```csharp
LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
// This date is within the range for timestamp32

var bin = MessagePackSerializer.Serialize(ldt);
// Once serialized we can expect the format to be [0xd6, -1, data] (format, extension type, data in bytes),
// and ‘bin’ to be a byte array of size 6
```

#### Deserialization
In the same way we can support serialization from NodaTime (eg, LocalDate) to MessagePack (timestamp), the same is applied for deserialization. 

From a timestamp, we can deserialize into a LocalDate (if time part is 0), LocalDateTime or an Instant.

From the snippet of code in serialization, shown below is deserialization:
```csharp
var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);
// the data is put back into a LocalDateTime variable, coming from 'bin' in serialization
```
:heavy_exclamation_mark: Deserializing a LocalDateTime into a LocalDate, will not work if the time value is not 0.

### NodaTime serialized formats
#### Instant
When an Instant is serialized, like LocalDateTime and LocalDate, it goes to timestamp format. Depending on the value of the Instant, 
it will fall into either timestamp 32, 64, or 96 format, as explained [above](https://github.com/ARKlab/MessagePack/blob/feature/Create-Nodatime-Extensions/README.md#timestamps).

#### LocalDate
Once a LocalDate is serialized it is in timestamp format. Depending on the value of the LocalDate, it will fall into either 
timestamp 32, 64 or 96. LocalDate has no time values.

#### LocalDateTime
Once a LocalDate is serialized it is in timestamp format. Depending on the value of the LocalDate, it will fall into either 
timestamp 32, 64 or 96. LocalDateTime can be deserialized into a LocalDate if it has no time part.

#### LocalTime
LocalTime is serialized into an int64 (64 bit int) from. The int64 contains the LocalTime value in nanoseconds.

#### Offset
Offset is serialized into an int32 (32 bit int). The int32 contains the Offset value in seconds.

#### OffsetDateTime
When an Offset is serialized, it split up into into the Offset and LocalDateTime parts.
They are then serialized using there respective formatters. 
This means the serialized OffsetDateTime will be put into an array of 2 elements.
The Offset and LocalDateTime serialization is explained in the headings above.

#### Period
When the NodaTime type Period is serialized, it is split into a 'fixarray'. 
For a Period we have a 10 element array of four int32 amd six int64 respectively, represented in the sort order of - 
Years, Months, Weeks, Days, Hours, Minutes, Seconds, Milliseconds, Ticks, Nanoseconds.

#### ZonedDateTime
A ZonedDateTime is split up into LocalDateTime, an Offset and a string representing a Zone, during serialization.
This means the ZonedDateTime is put into an array of 3 elements.
Each NodaTime type is serialized using there respective formatters, 
while the string is serialized using the MessagePack base class into a 'fixstr'.

## Contributing
Please read [CONTRIBUTING.md]() for details on our code of conduct, and the process for submitting pull requests to us.

## Links
* [Nuget]()
* [Github](https://github.com/ARKlab/MessagePack)
* [Ark Energy](http://www.ark-energy.eu/)


## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/ARKlab/MessagePack/blob/feature/Create-Nodatime-Extensions/LICENSE) file for details

## Acknowledgments
* [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp)
* [NodaTime](https://nodatime.org/)
* [MsgPack Spec](https://github.com/msgpack/msgpack/blob/master/spec.md)
