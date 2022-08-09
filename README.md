# FileWatcher
### Это приложение для просмотра содержимого выбранной директории
#### Возможности:
- Просмотр актуального списка файлов выбранной директории
- Кнопка повышения привилегий приложения
- Запуск файлов по двойному клику
- Список файлов демонстрирует имя файла, размер, полный путь к файлу, дату
#### Реализация:
- Клиентское приложение, реализованное на WPF MVVM, в котором реализованы выбор директории, список файлов, кнопка повышения привилегий и запуск файлов по двойному клику.
-	DLL, реализованная на C++, которая содержит проверку уровня привилегий, запуск процесса с привилегиями администратора, получение списка файлов, отслеживание изменений файлов.
- Взаимодействие между клиентским приложением и DLL реализовано через механизм Platform Invoke
- Реализован запрет на запуск двух экземпляров