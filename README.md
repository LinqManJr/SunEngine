# SunEngine

Движок сайта - форум, статьи, блог.

<img src="https://github.com/Dmitrij-Polyanin/SunEngine/blob/master/Client/src/statics/SunEngine.svg" width="250" alt="SunEngine Logo" />

Версия: 0.8.5

Демо: [demo.sunengine.site](http://demo.sunengine.site)  

#### Технологии:
- Asp.Net Core 2.2
- Linq2db - доступ к базе данных
- FluentMigrator - миграции
- VueJs - SPA клиентская часть
- Quasar Framework - компоненты vue
- База данных - любая совместимая с Linq2db и FluentMigrator  

### О проекте
#### Модули:
- Статьи
- Форум
- Блоги

#### Дружественный интерфейс
- Одностраничное приложение c красивым интерфейсом
- Mobile ready  

#### Сделано с любовью ❤
- Красивый код, который я постоянно улучшаю и совершенствую.
- Если что-то ещё в плане кода не настолько красиво как могло бы быть, принимается обратная связь.

#### Быстрая работа (quick & fast)
- Быстрый доступ к данным на основе linq2db.  
- Приложение грузит только то что нужно, без запроса всех данных при каждом запросе. SPA. 
- Эффективное кэширование

#### Гибкая настройка прав пользователей.
Можно настроить для любой группы пользователей для любого раздела сайта (категории) права на доступ на конкретные операции (Добавлять тексты, Добавлять сообщения, Редактировать и тд.)

## Ближайшие планы
- Модульная система
- Рефакторинг кода
- Админка
- Улучшение кэширования

## Контакты  
- Дмитрий Полянин  
- Telegram: [@okeanij](https://t.me/Okeanij)    
- Группа проекта в Telegram: [https://t.me/SunEngine](https://t.me/SunEngine) 

## Проект находится в стадии разработки
Желающие 
- Потестировать
- Предложить идеи по коду  

Пишите в группу Telegram или мне, либо сюда на github.

## Инсталляция
#### Перед инсталяцией должны быть установлены
- [.Net Core 2.2 SDK](https://dotnet.microsoft.com/download)
- [NodeJs](https://nodejs.org/en/download/)
- [Npm](https://www.npmjs.com)
- [Quasar-CLI](https://quasar-framework.org/guide/quasar-cli.html)  `npm install -g quasar-cli`

#### Установка и запуск
- Клонировать репозиторий SunEngine с GitHub.
- Создать на PostgreSQL пустую базу c названием `<DataBaseName>`.
- Указать название базы и `ConnectionString` в файле `SunEngine/DataBaseConnection.json`.
- Запустить проект Migrations. Произойдёт создание таблиц.
- Запустить проект DataSeedDev. Заполнение таблиц тестовыми данными.
- Компилируем и запускаем серверную часть Asp.Net Core.  
  - Компилируем и запускаем проект SunEngine.
- Компилируем и запускаем клиентскую часть.  
  - Зайти в коность в директорию `SunEngine/Client`
  - `quasar dev` 
  - Откроется браузер с сайтом
- Если что-то не работает написать мне.

#### Использование других баз данных

 - Для MySql дополнительные пакеты подключать не надо, они уже подключены. 
 - Для других DB подключить NuGet пакеты поддержки этой DB для
   - [FluentMigrator](https://fluentmigrator.github.io) в проекте `Migrations`.
   - [Linq2db](https://github.com/linq2db/linq2db) в проектах `SunEngine`,`SunEngine.Commons`,`DataSeedDev`.
 - Прописать поддержку в файле `Migrations/Main.cs`.  
   Строку `.AddPostgres()` заменяем на нужное.
 - На данный момент тестировалось с Postgres и MySql.


#### Лицензия

Кратко: 

- Без оплаты для некоммерческого использования.
- Платно для коммерческого использования.

[Ссылка на текст лицензии](licence.md)