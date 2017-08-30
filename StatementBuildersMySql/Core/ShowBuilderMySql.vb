Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class ShowBuilderMySql

		Inherits ShowBuilderBase

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
		Public Overrides Function ShowDatabases(ByVal sorted As Boolean) As String()
			Return ShowDatabases(DbResultMySql.DefaultConnectionString, sorted)
		End Function

		Public Overrides Function ShowDatabases _
		(ByVal connectionString As String, ByVal sorted As Boolean) As String()

			Dim sb = New SelectBuilderMySql
			sb.Select.Add("SCHEMA_NAME")
			sb.From.Add("information_schema.SCHEMATA")

			If sorted Then sb.Order.Add("SCHEMA_NAME")

			Return DbResultMySql.Instance.GetFieldList _
			(Of String)(connectionString, sb.ToString).ToArray
		End Function

		Public Overrides Function ShowDatabases() As String()
			Return ShowDatabases(DbResultMySql.DefaultConnectionString)
		End Function

		Public Overrides Function ShowDatabases(ByVal connectionString As String) As String()

			Return ShowDatabases(connectionString, False)
		End Function

		Public Overrides Function ShowTables _
		(ByVal databaseName As String, ByVal sorted As Boolean) As String()
			Return ShowTables(DbResultMySql.DefaultConnectionString, databaseName, sorted)
		End Function

		Public Overrides Function ShowTables _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal sorted As Boolean) As String()

			Dim sb = New SelectBuilderMySql
			sb.Select.Add("TABLE_NAME")
			sb.From.Add("information_schema.TABLES")
			sb.Where.Add("TABLE_SCHEMA = '" & databaseName & "'")

			If sorted Then
				sb.Order.Add("TABLE_NAME")
			End If

			Return DbResultMySql.Instance.GetFieldList(Of String) _
			(connectionString, sb.ToString).ToArray
		End Function

		Public Overrides Function ShowTables(ByVal databaseName As String) As String()
			Return ShowTables(DbResultMySql.DefaultConnectionString, databaseName)
		End Function

		Public Overrides Function ShowTables _
		(ByVal connectionString As String _
		, ByVal databaseName As String) As String()

			Return ShowTables(connectionString, databaseName, False)
		End Function

		Public Overrides Function ShowColumns _
		(ByVal databaseName As String _
		, ByVal tableName As String _
		, ByVal sorted As Boolean) As String()

			Return ShowColumns(DbResultMySql.DefaultConnectionString, databaseName, tableName, sorted)
		End Function

		Public Overrides Function ShowColumns _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String _
		, ByVal sorted As Boolean) As String()

			Dim sb = New SelectBuilderMySql
			sb.Select.Add("COLUMN_NAME")
			sb.From.Add("information_schema.COLUMNS")
			sb.Where.Add("(TABLE_SCHEMA = '" & databaseName & "')")
			sb.Where.Add("AND (TABLE_NAME = '" & tableName & "')")

			If sorted Then sb.Order.Add("COLUMN_NAME")

			Return DbResultMySql.Instance.GetFieldList(Of String) _
			(connectionString, sb.ToString).ToArray
		End Function

		Public Overrides Function ShowColumns _
		(ByVal databaseName As String, ByVal tableName As String) As String()

			Return ShowColumns(DbResultMySql.DefaultConnectionString, databaseName, tableName)
		End Function

		Public Overrides Function ShowColumns _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String) As String()

			Return ShowColumns(connectionString, databaseName, tableName, False)
		End Function

		Public Overrides Function ShowViews _
		(ByVal databaseName As String, ByVal sorted As Boolean) As String()

			Return ShowViews(DbResultMySql.DefaultConnectionString, databaseName, sorted)
		End Function

		Public Overrides Function ShowViews _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal sorted As Boolean) As String()

			Dim sb = New SelectBuilderMySql
			sb.Select.Add("TABLE_NAME")
			sb.From.Add("information_schema.VIEWS")
			sb.Where.Add("TABLE_SCHEMA = '" & databaseName & "'")

			If sorted Then
				sb.Order.Add("TABLE_NAME")
			End If

			Return DbResultMySql.Instance.GetFieldList(Of String) _
			(connectionString, sb.ToString).ToArray
		End Function

		Public Overrides Function ShowViews(ByVal databaseName As String) As String()

			Return ShowViews(DbResultMySql.DefaultConnectionString, databaseName)
		End Function

		Public Overrides Function ShowViews _
		(ByVal connectionString As String _
		, ByVal databaseName As String) As String()

			Return ShowViews(connectionString, databaseName, False)
		End Function

		Public Overrides Function ShowTriggers _
		(ByVal databaseName As String, ByVal sorted As Boolean) As String()

			Return ShowTriggers(DbResultMySql.DefaultConnectionString, databaseName, sorted)
		End Function

		Public Overrides Function ShowTriggers _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal sorted As Boolean) As String()

			Dim sb = New SelectBuilderMySql
			sb.Select.Add("TRIGGER_NAME")
			sb.From.Add("information_schema.TRIGGERS")
			sb.Where.Add("TRIGGER_SCHEMA = '" & databaseName & "'")

			If sorted Then sb.Order.Add("TRIGGER_NAME")

			Return DbResultMySql.Instance.GetFieldList(Of String) _
			(connectionString, sb.ToString).ToArray
		End Function

		Public Overrides Function ShowTriggers(ByVal databaseName As String) As String()


			Return ShowTriggers(DbResultMySql.DefaultConnectionString, databaseName)
		End Function

		Public Overrides Function ShowTriggers _
		(ByVal connectionString As String, ByVal databaseName As String) As String()

			Return ShowTriggers(connectionString, databaseName, False)
		End Function

		Public Overrides Function ShowIndexes _
		(ByVal databaseName As String, ByVal tableName As String, ByVal sorted As Boolean) As String()

			Return ShowIndexes(DbResultMySql.DefaultConnectionString, databaseName, tableName, sorted)
		End Function

		Public Overrides Function ShowIndexes _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String _
		, ByVal sorted As Boolean) As String()

			Dim sb = New SelectBuilderMySql
			sb.Select.Add("INDEX_NAME")
			sb.From.Add("information_schema.STATISTICS")
			sb.Where.Add("(TABLE_SCHEMA = '" & databaseName & "')")
			sb.Where.Add("AND (TABLE_NAME = '" & tableName & "')")
			sb.Group.Add("INDEX_NAME")
			If sorted Then sb.Order.Add("INDEX_NAME")

			Return DbResultMySql.Instance.GetFieldList(Of String) _
			(connectionString, sb.ToString).ToArray
		End Function

		Public Overrides Function ShowIndexes _
		(ByVal databaseName As String, ByVal tableName As String) As String()

			Return ShowIndexes(DbResultMySql.DefaultConnectionString, databaseName, tableName)
		End Function

		Public Overrides Function ShowIndexes _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String) As String()

			Return ShowIndexes(connectionString, databaseName, tableName, False)
		End Function

		Public Overrides Function ShowCreateDataBase(ByVal databaseName As String) As String

			Return ShowCreateDataBase(DbResultMySql.DefaultConnectionString, databaseName)
		End Function

		Public Overrides Function ShowCreateDataBase _
		(ByVal connectionString As String, ByVal databaseName As String) As String

			Dim s = "SHOW CREATE DATABASE " & databaseName
			Dim result = ""

			Using dr = DbResultMySql.Instance.ExecuteReader(connectionString, s)
				While dr.Read
					result = dr.GetString(1)
				End While
			End Using
			Return result
		End Function

		Public Overrides Function ShowCreateTable _
		(ByVal databaseName As String, ByVal tableName As String) As String

			Return ShowCreateTable(DbResultMySql.DefaultConnectionString, databaseName, tableName)
		End Function

		Public Overrides Function ShowCreateTable _
		(ByVal connectionString As String _
		, ByVal databaseName As String _
		, ByVal tableName As String) As String

			Dim s = "SHOW CREATE TABLE " & databaseName & "." & tableName
			Dim result = ""

			Using dr = DbResultMySql.Instance.ExecuteReader(connectionString, s)
				While dr.Read
					result = dr.GetString(1)
				End While
			End Using
			Return result
		End Function
#End Region

	End Class

End Namespace