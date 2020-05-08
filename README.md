# Roulette Project

Esta es una API hecha en C#, .NET CORE 3.1, MongoDB, HAProxy, Docker y JWT Authentication. Consiste en un juego de apuesta en el que uno puede crear una ruleta, abrirla, hacer apuestas con diferentes usuarios, y cerrar la ruleta para que automáticamente de un número y color ganador. El proyecto está diseñado para ser escalable, la arquitectura elegida es 2-Tier con un load balancer, un servidor y una base de datos con escalabilidad flexible. Se utiliza Authorization para la mayoría de los endpoint, así que se debe utilizar JWT Bearer en el Header. Es aconsejable utilizar Postman.

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
| POST http://localhost:8080/api/v1/users/register | Registro público de usuario |
| POST http://localhost:8080/api/v1/users/authenticate | Autentificación pública de usuario para obtener JWT |
| POST http://localhost:8080/api/v1/roulettes | Petición privada para crear una nueva ruleta, no requiere body |
| PUT http://localhost:8080/api/v1/roulettes/open | Petición privada para abrir una ruleta, require id en el body |
| PUT http://localhost:8080/api/v1/roulettes/bet | Petición privada para hacer una apuesta, requiere color, money, number, RouletteId en el body|
| GET http://localhost:8080/api/v1/roulettes | Petición privada para obtener todas las ruletas |
| PUT http://localhost:8080/api/v1/roulettes/close | Petición privada para cerrar una ruleta y ejecutar el spin para obtener un númeroy color  ganador, requiere id en el body |

## Otros Endpoints
| URL | Service |
|-----|---------|
| GET http://localhost:8080/api/v1/roulettes/{id} | Petición privada para obtener una ruleta por id |
| DETELE http://localhost:8080/api/roulettes/{id} | Petición privada para eliminar una ruleta por id |
| GET http://localhost:8080/api/v1/users | Petición privada para obtener todos los usuarios |
| GET http://localhost:8080/api/v1/users/{id} | Petición privada para obtener un usuario por id |

<a name="Folder"></a>
## Folder Structure
```
├── build.sh
├── docker-compose.yml
├── haproxy
│   └── haproxy.cfg
├── README.md
└── RouletteAPI
    ├── appsettings.Development.json
    ├── appsettings.json
    ├── Controllers
    │   ├── RoulettesController.cs
    │   └── UserController.cs
    ├── Data
    │   ├── RouletteContext.cs
    │   ├── RouletteRepository.cs
    │   ├── UserContext.cs
    │   └── UserRepository.cs
    ├── Dockerfile
    ├── Helpers
    │   ├── AppException.cs
    │   ├── AppSettings.cs
    │   ├── AutoMapperProfile.cs
    │   ├── BetHandler.cs
    │   ├── JwtHandler.cs
    │   ├── PasswordHandler.cs
    │   ├── RouletteHandler.cs
    │   └── UserHandler.cs
    ├── Interfaces
    │   ├── IRouletteRepository.cs
    │   └── IUserRepository.cs
    ├── Models
    │   ├── AuthenticateModel.cs
    │   ├── BetRoulette.cs
    │   ├── Entities
    │   │   ├── Roulette.cs
    │   │   └── User.cs
    │   ├── RegisterModel.cs
    │   ├── Settings.cs
    │   └── UserModel.cs
    ├── obj
    ├── ├── ...
    ├── Program.cs
    ├── Properties
    │   └── launchSettings.json
    ├── RouletteApi.csproj
    └── Startup.cs

```

<a name="Requirements"></a>
## Requirements
* [Docker Engine 17.06+](https://docs.docker.com/engine/installation/)
* [Docker Compose 1.8+](https://docs.docker.com/compose/install/)

<a name="Instalation"></a>
## Instalation
Clone into your project:
```bash
git clone https://github.com/vargas88hugo/roulette.git
```

<a name="Usage"></a>
## Usage
Para preparar el proyecto en Docker se puede utilizar el bash script en linux. Si desea correrlo localmente hay un branch configurado.
```bash
cd roulette
./build.sh
```

<a name="TODO"></a>
## TODO
* Implementar servicios para desacoplar los repositorios.
* Implementar una mejor arquitectura como DDD.
* Hay algunas respuestas y errores que aún deben ser manejados.
* Configuración de master-slave en la base de datos.
* Persistencia del cache en el Load Balancer.
