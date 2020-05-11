<div align="center">
<img align="center" src="https://canberra-party-hire.com.au/wp-content/uploads/2016/06/CANBERRA-CASINO-HIRE.png" />
</div>

# Roulette Project
Esta es una API hecha en C#, .NET CORE 3.1, MongoDB, HAProxy, Docker y JWT Authentication. Consiste en un juego de apuesta en el que uno puede crear una ruleta, abrirla, hacer apuestas con diferentes usuarios, y cerrar la ruleta para que automáticamente de un número y color ganador. El proyecto está diseñado para ser escalable, la arquitectura elegida es 2-Tier con un load balancer, un servidor y una base de datos con escalabilidad flexible. Se utiliza Authorization para la mayoría de los endpoint, así que se debe utilizar JWT Bearer en el Header. Es aconsejable utilizar Postman.

## Version 2
* Desacoplamiento de repositorios con el controlador. Ahora hay una capa intermedia de servicios orientado a interfaces.
* Corrección de errores en docker y optimización.
* Mejoramiento de respuestas exitosas y de errores.
* Utilización de Data Annotations.
* Refactorización y Clean Code.

## IMPORTANTE: 
Ahora hay dos entornos de desarrollo que se conectan al mismo puerto del host. En ocasiones la imagen de .net tiene problemas de conectividad.

## Contents
- [Endpoints](#Endpoints)
- [Folder Structure](#Folder)
- [Requirements](#Requirements)
- [Instalation](#Instalation)
- [Usage](#Usage)
- [TODO](#TODO)

<a name="Endpoints"></a>
## Main Endpoints
| URL | Service |
|-----|---------|
| POST http://localhost:5000/api/v2/auth/register | Registro público de usuario. Requiere username, password y money en el body |
| POST http://localhost:5000/api/v2/auth/authenticate | Autentificación pública de usuario para obtener JWT. Requiere username y password |
| POST http://localhost:5000/api/v2/roulette | Petición para crear una nueva ruleta, no requiere body |
| PUT http://localhost:5000/api/v2/roulette/open | Petición privada para abrir una ruleta, require RouletteId en el body. Esta acción reinicia las apuestas de la ruleta |
| PUT http://localhost:5000/api/v2/bet/makebet | Petición privada para hacer una apuesta, requiere color, money, number, RouletteId en el body. | 
| PUT http://localhost:5000/api/v2/bet/close | Petición privada para cerrar una ruleta y ejecutar el spin para obtener un número y color  ganador. Requiere RouletteId en el body. Esta acción solo se puede realizar si la ruleta está abierta |
| GET http://localhost:5000/api/v2/roulette | Petición privada para obtener todas las ruletas |


## Otros Endpoints
| URL | Service |
|-----|---------|
| GET http://localhost:5000/api/v2/roulette/{id} | Petición privada para obtener una ruleta por id |
| GET http://localhost:5000/api/v2/user | Petición privada para obtener todos los usuarios |
| GET http://localhost:5000/api/v2/user/{id} | Petición privada para obtener un usuario por id |

<a name="Folder"></a>
## Folder Structure
```
├── appsettings.Development.json
├── appsettings.json
├── Controllers
│   ├── AuthController.cs
│   ├── BetController.cs
│   ├── RouletteController.cs
│   └── UserController.cs
├── Dockerfile
├── Helpers
│   ├── AppSettings.cs
│   ├── AuthHelper.cs
│   ├── AutoMapperProfile.cs
│   ├── JwtHelper.cs
│   ├── RouletteHelper.cs
│   ├── StartupHelper.cs
│   └── UserHelper.cs
├── Interfaces
│   ├── IAuthService.cs
│   ├── IBetService.cs
│   ├── IRouletteRepository.cs
│   ├── IRouletteService.cs
│   ├── IUserRepository.cs
│   └── IUserService.cs
├── Models
│   ├── AuthenticateModel.cs
│   ├── BetMessageModel.cs
│   ├── BetModel.cs
│   ├── CloseOpenModel.cs
│   ├── Entities
│   │   ├── BetRoulette.cs
│   │   ├── Roulette.cs
│   │   └── User.cs
│   ├── MakeBetMessageModel.cs
│   ├── RegisterModel.cs
│   └── Settings.cs
├── obj
│   ├── ...
├── Program.cs
├── Properties
│   └── launchSettings.json
├── Repositories
│   ├── RouletteContext.cs
│   ├── RouletteRepository.cs
│   ├── UserContext.cs
│   └── UserRepository.cs
├── RouletteApi.csproj
├── Services
│   ├── AuthService.cs
│   ├── BetService.cs
│   ├── RouletteService.cs
│   └── UserService.cs
└── Startup.cs

```

<a name="Requirements"></a>
## Requirements
### Docker
* [Docker Engine 17.06+](https://docs.docker.com/engine/installation/)
* [Docker Compose 1.8+](https://docs.docker.com/compose/install/)
### Local
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [MongoDB 4.2](https://docs.mongodb.com/manual/administration/install-community/)

<a name="Instalation"></a>
## Instalation
Clone into your project:
```bash
git clone https://github.com/vargas88hugo/roulette.git
```

<a name="Usage"></a>
## Usage
Hay un entorno local y otro dockerizado. Para correr el programa sigua cualquiera de los comandos en Linux/Windows una vez que clone el repositorio en el fichero actual:

### Local
```bash
cd roulette/local/RouletteApi/
dotnet run
```

### Docker
```
cd roulette/docker
docker-compose up --build
```

<a name="TODO"></a>
## TODO
* Implementar una mejor arquitectura como DDD.
* Se deben configurar diferentes status de código a las respuestas de errores.
* Configuración de master-slave en la base de datos y posible CQRS.
* Persistencia del cache en el Load Balancer.
