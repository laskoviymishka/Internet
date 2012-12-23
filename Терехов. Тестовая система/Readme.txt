В папке Package находятся скомпилированные файлы проекта а так же инструкция по их установки.
В архиве Internet содержится исходный проект, а так же база данных
В корне находятся бэкапы базы данных для развертывания на сервере. База данных разрабатывалась на MS SQL Server 2012. 
Для восстоновления базы данных из бэкапа можно воспользоваться скриптом
DECLARE @p AS varchar(100)
set @p = '`\TestDB.bak'

RESTORE FILELISTONLY
FROM         DISK = @p

RESTORE DATABASE staticdatadb
	FROM  DISK = @p
	WITH  FILE = 1,  
	MOVE 'TestDB' TO 'e:\loads\TestDB.mdf',  
	MOVE 'TestDB_log' TO 'e:\loads\TestDB.ldf',  
	NOUNLOAD,  STATS = 10

Либо воспользоваться одним из множества других способов:http://bit.ly/UXqKgy
							http://bit.ly/UXqNZP
							http://bit.ly/TduhsJ



Данный сайт развернут на бесплатном хостинге Somee.com (не реклама)
Адресс сайта http://laskoviymish.somee.com/ - открыт для тестирования, прошу проходите тесты только у нас ^^

Для справочной информации обращайтесь к создателю - скайп Laskoviy_Mishka