[![NuGet](https://img.shields.io/nuget/v/Digi21.Utilities?style=flat)](https://www.nuget.org/packages/Digi21.Utilities/)

# Digi21.Utilities

Este repositorio contiene métodos de extensión para los tipos enumerados en el ensamblado Digi21.DigiNG que facilitan la tarea de
desarrollar aplicaciones con Digi3D.AI.

Para utilizar los métodos aquí enumerados tan solo tienes que añadir a tu solución el paquete NuGet correspondiente:

`Install-Package Digi21.Utilities -Version 17.2.0`

## Publishing

Push a tag `v<version>` whose version matches `<Version>` in `Digi21.Utilities/Digi21.Utilities.csproj`. The *Release* workflow builds the assembly, packs it and publishes it to nuget.org with trusted publishing (repository secret `NUGET_USER`, the nuget.org profile name). The packages are not author-signed; the assembly is public-signed with `Digi21.PublicKey.snk`, which contains only the public key.
