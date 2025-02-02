# Electricity Management API

## Описание

Electricity Management API — это веб-сервис для управления данными, связанными с объектами электроснабжения. Включает поддержку организаций, объектов потребления, точек поставки, измерительных точек и приборов электроснабжения.

Проект разработан на **ASP.NET Core 5+** с использованием **Entity Framework Core** (In-Memory Database) и **Swagger** для удобной документации API.

## Схема базы данных
![Схема базы данных](database%20design/Database%20schema.jpg)


## Функциональность

- Управление организациями и дочерними организациями
- Управление объектами потребления
- Управление точками поставки и измерительными точками
- Управление счетчиками электроэнергии и трансформаторами
- Документирование API с помощью Swagger

## Технологии

- **ASP.NET Core Web API**
- **Entity Framework Core** (In-Memory Database)
- **Swagger (Swashbuckle)**
- **Dependency Injection**
- **RESTful API**

## Запуск

По умолчанию API запускается на **<http://localhost:8050>**.

### Открытие Swagger UI

После запуска API Swagger UI будет доступен по адресу:

```sh
http://localhost:8050/swagger/index.html
```

## Конфигурация

### Файл `launchSettings.json`

В проекте настроены два профиля:

- **Local** — запускает API с включённым Swagger (`http://localhost:8050`)
- **Prod** — запускает API без Swagger

## API Эндпоинты

#### Для всех сущностей реализованы CRUD-операции.

#### Добавлены API Эндпоинты по УТЗ:

1. Добавить новую точку измерения с указанием счетчика, трансформатора тока и трансформатора напряжения.

    - ****POST /api/measurement-points/organizations/add-with-devices****

2. Выбрать все расчетные приборы учета в 2018 году.
    - ****GET /api/calculation-meters/year/{year}****

3. По указанному объекту потребления выбрать все счетчики с закончившимся сроком поверке.
    - ****GET /api/electricity-meters/expired/{objectId}****
4. По указанному объекту потребления выбрать все трансформаторы напряжения с закончившимся сроком поверке.
    - ****GET /api/voltage-transformers/expired/{objectId}****
5. По указанному объекту потребления выбрать все трансформаторы тока с закончившимся сроком поверке.
    - ****GET /api/current-transformers/expired/{objectId}****
