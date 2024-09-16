# Redis Benchmark CLI

Проект для бенчмаркинга Redis с использованием .NET 8.0. Состоит из отправителя и нескольких подписчиков, которые измеряют задержку сообщений через Redis Pub/Sub.

## Требования

- Docker и Docker Compose
- .NET SDK 8.0 (для локального запуска)

## Быстрый старт

1. **Клонируйте репозиторий:**

   ```bash
   git clone https://github.com/Anarant123/RedisBenchmark
   cd redis-benchmark-cli
   ```

2. **Проверьте `appsettings.json`:**

   ```json
   {
     "ConnectionString": "redis-server:6379"
   }
   ```

3. **Запустите Docker Compose:**

   ```bash
   docker-compose up --build
   ```

4. **Остановите контейнеры при необходимости:**

   ```bash
   docker-compose down
   ```

## Конфигурация

- **ConnectionString**: строка подключения к Redis (для Docker — `redis-server:6379`).

## Локальный запуск

1. Убедитесь, что Redis запущен локально.
2. Измените `ConnectionString` на `localhost:6379` в `appsettings.json`.
3. Запустите приложение:

   ```bash
   dotnet run --project RedisBenchmark.CLI
   ```
