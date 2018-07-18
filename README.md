# Hometask 7 for binary-studio academy 18 

## Що зроблено:
* Попередній [проект](https://github.com/VoBilyk/Lesson6_AirportTest) переведено на використання асинхронних операції
* Тепер всі запити до контролерів виконуються асинхронно.
* Добавлено завантаження даних з [mockApi](http://5b128555d50a5c0014ef1204.mockapi.io/crew) за допомогою _GET api/crews/mockapi_
* При завантаженні даних з mockApi вони зберігаються асинхронно в базі даних і лог файлів в папці logs WebApi проекту.
* Реалізовано імітацію фіктивної загрузки всіх елементів flights за допомогою _GET: api/flights/await?delay:value_, де value - час в мілісикундах 
