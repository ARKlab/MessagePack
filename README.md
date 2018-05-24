![image](http://www.ark-energy.eu/wp-content/uploads/ark-dark.png)
# MessagePack.NodaTime

This library adds MessagePack support for NodaTime types.

## Getting Started

### Installation

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
As per the MessagePack spec, when we serialize a NodaTime type of LocalDateTime or LocalDate, an extension type of -1 is received meaning it is a MessagePack timestamp.

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
var res = MessagePackSerializer.Deserialize<LocalDateTime>(ldt);
// the data is put back into a LocalDateTime variable
```
:heavy_exclamation_mark: Deserializing a LocalDateTime into a LocalDate, will not work if the time value is not 0.
## Contributing

Please read [CONTRIBUTING.md]() for details on our code of conduct, and the process for submitting pull requests to us.

## Links
* [Nuget]()
* [Github](https://github.com/ARKlab/MessagePack)
* [Ark Energy](http://www.ark-energy.eu/)


## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/ARKlab/MessagePack/blob/feature/Create-Nodatime-Extensions/LICENSE) file for details

## Acknowledgments

* [MessagePack](https://github.com/neuecc/MessagePack-CSharp)
* [NodaTime](https://nodatime.org/)
