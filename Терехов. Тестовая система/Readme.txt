� ����� Package ��������� ���������������� ����� ������� � ��� �� ���������� �� �� ���������.
� ������ Internet ���������� �������� ������, � ��� �� ���� ������
� ����� ��������� ������ ���� ������ ��� ������������� �� �������. ���� ������ ��������������� �� MS SQL Server 2012. 
��� �������������� ���� ������ �� ������ ����� ��������������� ��������
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

���� ��������������� ����� �� ��������� ������ ��������:http://bit.ly/UXqKgy
							http://bit.ly/UXqNZP
							http://bit.ly/TduhsJ



������ ���� ��������� �� ���������� �������� Somee.com (�� �������)
������ ����� http://laskoviymish.somee.com/ - ������ ��� ������������, ����� ��������� ����� ������ � ��� ^^

��� ���������� ���������� ����������� � ��������� - ����� Laskoviy_Mishka