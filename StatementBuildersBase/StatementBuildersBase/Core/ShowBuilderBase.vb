Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core

	Public MustInherit Class ShowBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		Public MustOverride Function ShowDatabases(ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowDatabases(ByVal connectionString As String, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowDatabases() As String()

		Public MustOverride Function ShowDatabases(ByVal connectionString As String) As String()

		Public MustOverride Function ShowTables _
		(ByVal databaseName As String, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowTables _
		(ByVal connectionString As String, ByVal databaseName As String, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowTables(ByVal databaseName As String) As String()

		Public MustOverride Function ShowTables(ByVal connectionString As String, ByVal databaseName As String) As String()

		Public MustOverride Function ShowColumns _
		(ByVal databaseName As String, ByVal tableName As String, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowColumns _
		(ByVal connectionString As String, ByVal databaseName As String _
		, ByVal tableName As String, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowColumns _
		(ByVal databaseName As String, ByVal tableName As String) As String()

		Public MustOverride Function ShowColumns _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String) As String()

		Public MustOverride Function ShowViews _
		(ByVal databaseName As String _
		, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowViews _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowViews(ByVal databaseName As String) As String()

		Public MustOverride Function ShowViews _
		(ByVal connectionString As String, ByVal databaseName As String) As String()

		Public MustOverride Function ShowTriggers _
		(ByVal databaseName As String, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowTriggers _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowTriggers(ByVal databaseName As String) As String()

		Public MustOverride Function ShowTriggers _
		(ByVal connectionString As String _
		, ByVal databaseName As String) As String()

		Public MustOverride Function ShowIndexes _
		(ByVal databaseName As String _
		, ByVal tableName As String _
		, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowIndexes _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String _
		, ByVal sorted As Boolean) As String()

		Public MustOverride Function ShowIndexes _
		(ByVal databaseName As String _
		, ByVal tableName As String) As String()

		Public MustOverride Function ShowIndexes _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String) As String()

		Public MustOverride Function ShowCreateDataBase(ByVal databaseName As String) As String

		Public MustOverride Function ShowCreateDataBase _
		(ByVal connectionString As String _
		, ByVal databaseName As String) As String

		Public MustOverride Function ShowCreateTable _
		(ByVal databaseName As String, ByVal tableName As String) As String

		Public MustOverride Function ShowCreateTable _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String) As String
#End Region

	End Class
End Namespace