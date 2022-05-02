# Speckle .NET Starter

[![Twitter Follow](https://img.shields.io/twitter/follow/SpeckleSystems?style=social)](https://twitter.com/SpeckleSystems) [![Community forum users](https://img.shields.io/discourse/users?server=https%3A%2F%2Fdiscourse.speckle.works&style=flat-square&logo=discourse&logoColor=white)](https://discourse.speckle.works) [![website](https://img.shields.io/badge/https://-speckle.systems-royalblue?style=flat-square)](https://speckle.systems) [![docs](https://img.shields.io/badge/docs-speckle.guide-orange?style=flat-square&logo=read-the-docs&logoColor=white)](https://speckle.guide/dev/)

## Introduction

This project is an example to showcase how to deal with local accounts, sending, receiving and creating new commits on a Speckle server using our C# SDK.

> If you're trying to run this you should have the .NetCore3.1 Runtime installed

### Running

This is a `dotnet core 3.1` console app, so just run:

```shell
dotnet run
```

### Creating your own

This is quite simple, just create a new console app:

```sh
dotnet new console -f netcoreapp3.1
```

And then add `Speckle.Core` as a dependency

```sh
dotnet add package Speckle.Core --version 2.5.2
```

And `Speckle.Objects` if you want to be able to create speckle specific objects.

```sh
dotnet add package Speckle.Core --version 2.5.2
```

## Documentation

Comprehensive developer and user documentation can be found in our:

#### 📚 [Speckle Docs website](https://speckle.guide/dev/)

## Contributing

Please make sure you read the [contribution guidelines](.github/CONTRIBUTING.md) for an overview of the best practices we try to follow.

## Community

The Speckle Community hangs out on [the forum](https://discourse.speckle.works), do join and introduce yourself & feel free to ask us questions!

## Security

For any security vulnerabilities or concerns, please contact us directly at security[at]speckle.systems.

## License

Unless otherwise described, the code in this repository is licensed under the Apache-2.0 License. Please note that some modules, extensions or code herein might be otherwise licensed. This is indicated either in the root of the containing folder under a different license file, or in the respective file's header. If you have any questions, don't hesitate to get in touch with us via [email](mailto:hello@speckle.systems).
