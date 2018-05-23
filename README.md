![image](http://www.ark-energy.eu/wp-content/uploads/ark-dark.png)
# MessagePack.NodaTime

This library adds MessagePack support for Nodatime types.

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


## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Links
* [Nuget]()
* [Github](https://github.com/ARKlab/MessagePack)
* [Ark Energy](http://www.ark-energy.eu/)


## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/ARKlab/MessagePack/blob/feature/Create-Nodatime-Extensions/LICENSE) file for details

## Acknowledgments

* [MessagePack](https://github.com/neuecc/MessagePack-CSharp)
* [NodaTime](https://nodatime.org/)
