Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Data.Common
#End Region

Namespace Core

	Public Class DbResultBase '(Of TProviderFactory As {New, DbProviderFactory})

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _providerFactory As DbProviderFactory
		Private _con As DbConnection
		Private _trans As DbTransaction
		Private _getLastInsertIdStatement As String
		Private _defaultConnectionString As String
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		'''<summary>Erzeugt ein DbResultBase-Objekt unter Angabe des DbProviderFactory-Typs TProviderFactory.</summary>
		Public Shared Function CreateDbResultBaseObject _
		(Of TProviderFactory As {New, DbProviderFactory}) _
		(ByVal getLastInsertIdStatement As String) As DbResultBase

			Return CreateDbResultBaseObject(New TProviderFactory, "", getLastInsertIdStatement)
		End Function

		'''<summary>Erzeugt ein DbResultBase-Objekt unter Angabe des DbProviderFactory-Typs TProviderFactory.</summary>
		Public Shared Function CreateDbResultBaseObject _
		(Of TProviderFactory As {New, DbProviderFactory}) _
		(ByVal defaultConnectionString As String _
		, ByVal getLastInsertIdStatement As String) As DbResultBase

			Return CreateDbResultBaseObject _
			(New TProviderFactory, defaultConnectionString, getLastInsertIdStatement)
		End Function

		'''<summary>Erzeugt ein DbResultBase-Objekt unter Angabe einer DbProviderFactory-instanz providerFactory.</summary>
		Public Shared Function CreateDbResultBaseObject _
		(ByVal providerFactory As DbProviderFactory _
		, ByVal getLastInsertIdStatement As String) As DbResultBase

			Return New DbResultBase(providerFactory, "", getLastInsertIdStatement)
		End Function

		'''<summary>Erzeugt ein DbResultBase-Objekt unter Angabe einer DbProviderFactory-instanz providerFactory.</summary>
		Public Shared Function CreateDbResultBaseObject _
		(ByVal providerFactory As DbProviderFactory _
		, ByVal defaultConnectionString As String _
		, ByVal getLastInsertIdStatement As String) As DbResultBase

			Return New DbResultBase(providerFactory, defaultConnectionString, getLastInsertIdStatement)
		End Function

		Private Sub New _
		(ByVal providerFactory As DbProviderFactory _
		, ByVal defaultConnectionString As String _
		, ByVal getLastInsertIdStatement As String)

			_providerFactory = providerFactory
			_defaultConnectionString = defaultConnectionString
			_getLastInsertIdStatement = getLastInsertIdStatement

		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
		Private Sub OnCloseConnection(ByVal sender As Object, ByVal e As StateChangeEventArgs)

			Dim connection = DirectCast(sender, DbConnection)

			If e.CurrentState = ConnectionState.Closed Then
				RemoveHandler connection.StateChange, AddressOf OnCloseConnection
				connection.Dispose()
			End If
		End Sub
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
		Private Function CreateConnection(ByVal connectionString As String) As DbConnection
			Dim connection = _providerFactory.CreateConnection()
			connection.ConnectionString = connectionString
			connection.Open()
			Return connection
		End Function

		Private Function CreateCommand _
		(ByVal connection As DbConnection, ByVal statement As String) As DbCommand

			Dim cmd = _providerFactory.CreateCommand
			cmd.Connection = connection
			cmd.CommandText = statement
			cmd.CommandTimeout = 0
			Return cmd
		End Function

		Private Function CreateCommand(ByVal connection As DbConnection _
		, ByVal statement As String, ByVal parameters() As IDbDataParameter) As DbCommand

			Dim cmd = CreateCommand(connection, statement)
			AddParameters(cmd, parameters)
			Return cmd
		End Function

		Private Function GetDataAdapter(ByVal cmd As DbCommand) As DbDataAdapter
			Dim da = _providerFactory.CreateDataAdapter()
			da.SelectCommand = cmd
			Return da
		End Function

		Private Function CreateDataSetByDataAdapter(ByVal cmd As DbCommand) As DataSet

			Using da = GetDataAdapter(cmd)
				Dim ds = New DataSet
				da.Fill(ds)
				Return ds
			End Using
		End Function

		Private Function CreateDataTableByDataAdapter(ByVal cmd As DbCommand) As DataTable

			Using da = GetDataAdapter(cmd)
				Dim dt = New DataTable
				da.Fill(dt)
				Return dt
			End Using
		End Function

		Private Sub AddParameters(ByVal cmd As IDbCommand, ByVal parameters() As IDbDataParameter)
			If parameters Is Nothing Then Return
			parameters.ToList.ForEach(Sub(p) cmd.Parameters.Add(p))
		End Sub
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "

#Region "ExecuteScalar"
		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteScalar(ByVal statement As String) As Object

			Return ExecuteScalar(_defaultConnectionString, statement, Nothing)
		End Function

		Public Function ExecuteScalar(ByVal connectionString As String, ByVal statement As String) As Object

			Return ExecuteScalar(connectionString, statement, Nothing)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteStringScalar(ByVal statement As String) As String

			Return ExecuteScalar(_defaultConnectionString, statement).ToString
		End Function

		Public Function ExecuteStringScalar(ByVal connectionString As String, ByVal statement As String) As String

			Return ExecuteScalar(connectionString, statement).ToString
		End Function

		Public Function ExecuteScalar(Of T)(ByVal connection As DbConnection _
		, ByVal statement As String, ByVal parameters() As IDbDataParameter) As T

			Return DirectCast(ExecuteScalar(connection, statement, parameters), T)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteScalar(Of T)(ByVal statement As String) As T

			Return ExecuteScalar(Of T)(_defaultConnectionString, statement, Nothing)
		End Function

		Public Function ExecuteScalar(Of T)(ByVal connectionString As String, ByVal statement As String) As T

			Return ExecuteScalar(Of T)(connectionString, statement, Nothing)
		End Function

		Public Function ExecuteScalar(Of T)(ByVal connection As DbConnection, ByVal statement As String) As T

			Return ExecuteScalar(Of T)(connection, statement, Nothing)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteScalar(Of T) _
		(ByVal statement As String, ByVal parameters() As IDbDataParameter) As T

			Return DirectCast(ExecuteScalar(_defaultConnectionString, statement, parameters), T)
		End Function

		Public Function ExecuteScalar(Of T)(ByVal connectionString As String _
		, ByVal statement As String, ByVal parameters() As IDbDataParameter) As T

			Return DirectCast(ExecuteScalar(connectionString, statement, parameters), T)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteScalar _
		(ByVal statement As String, ByVal parameters() As IDbDataParameter) As Object

			Using connection = CreateConnection(_defaultConnectionString)
				Return ExecuteScalar(connection, statement, parameters)
			End Using
		End Function

		Public Function ExecuteScalar _
		(ByVal connectionString As String, ByVal statement As String _
		, ByVal parameters() As IDbDataParameter) As Object

			Using connection = CreateConnection(connectionString)
				Return ExecuteScalar(connection, statement, parameters)
			End Using
		End Function

		Public Function ExecuteScalar _
		(ByVal connection As DbConnection _
		, ByVal statement As String _
		, ByVal parameters() As IDbDataParameter) As Object

			Using cmd = CreateCommand(connection, statement, parameters)
				Return cmd.ExecuteScalar
			End Using
		End Function
#End Region

#Region "ExecuteReader"
		Public Function ExecuteReader _
		(ByVal connection As DbConnection, ByVal statement As String) As DbDataReader

			Return ExecuteReader(connection, statement, Nothing)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteReader(ByVal statement As String) As DbDataReader

			Return ExecuteReader(_defaultConnectionString, statement, Nothing)
		End Function

		Public Function ExecuteReader _
		(ByVal connectionString As String, ByVal statement As String) As DbDataReader

			Return ExecuteReader(connectionString, statement, Nothing)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteReader _
		(ByVal statement As String, ByVal parameters() As IDbDataParameter) As DbDataReader

			Dim connection = CreateConnection(_defaultConnectionString)
			Return ExecuteReader(connection, statement, parameters)
		End Function

		Public Function ExecuteReader _
		(ByVal connectionString As String, ByVal statement As String _
		, ByVal parameters() As IDbDataParameter) As DbDataReader

			Dim connection = CreateConnection(connectionString)
			Return ExecuteReader(connection, statement, parameters)
		End Function

		Public Function ExecuteReader _
		(ByVal connection As DbConnection, ByVal statement As String _
		, ByVal parameters() As IDbDataParameter) As DbDataReader

			AddHandler connection.StateChange, AddressOf OnCloseConnection

			Using cmd = CreateCommand(connection, statement, parameters)
				Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
			End Using
		End Function
#End Region

#Region "GetFieldList"

		'''<summary>
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''Liefert bei einer Einfeldabfrage das abgefragte Feld als Liste des Typs T zurück.
		'''</summary>
		Public Function GetFieldList(Of T)(ByVal statement As String) As List(Of T)

			Return GetFieldList(Of T)(_defaultConnectionString, statement, "")
		End Function

		'''<summary>Liefert bei einer Einfeldabfrage das abgefragte Feld als Liste des Typs T zurück.</summary>
		Public Function GetFieldList(Of T) _
		(ByVal connectionString As String, ByVal statement As String) As List(Of T)

			Return GetFieldList(Of T)(connectionString, statement, "")
		End Function

		'''<summary>
		'''Liefert bei einer Abfrage das angegebene Feld als Liste des Typs T zurück.
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		Public Function GetFieldList_(Of T)(ByVal statement As String, ByVal field As String) As List(Of T)
			Return GetFieldList(Of T)(_defaultConnectionString, statement, field)
		End Function

		'''<summary>Liefert bei einer Abfrage das angegebene Feld als Liste des Typs T zurück.</summary>
		Public Function GetFieldList(Of T)(ByVal connectionString As String _
		, ByVal statement As String, ByVal field As String) As List(Of T)

			Dim list = New List(Of T)
			Using dr = ExecuteReader(connectionString, statement)
				If field = "" Then field = dr.GetName(0)
				While dr.Read
					list.Add(CType(dr.Item(field), T))
				End While
			End Using
			Return list
		End Function
#End Region

#Region "ExecuteNonQuery"
		Public Function ExecuteNonQuery(ByVal connection As DbConnection, ByVal statement As String) As Int64

			Return ExecuteNonQuery(connection, statement, Nothing)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteNonQuery(ByVal statement As String) As Int64

			Return ExecuteNonQuery(_defaultConnectionString, statement, Nothing)
		End Function

		Public Function ExecuteNonQuery _
		(ByVal connectionString As String, ByVal statement As String) As Int64

			Return ExecuteNonQuery(connectionString, statement, Nothing)
		End Function

		'''<summary>Benutzt den hinterlegten DefaultConnectionString.</summary>
		Public Function ExecuteNonQuery(ByVal statement As String _
		, ByVal parameters() As IDbDataParameter) As Int64

			Using connection = CreateConnection(_defaultConnectionString)
				Return ExecuteNonQuery(connection, statement, parameters)
			End Using
		End Function

		Public Function ExecuteNonQuery(ByVal connectionString As String _
		, ByVal statement As String, ByVal parameters() As IDbDataParameter) As Int64

			Using connection = CreateConnection(connectionString)
				Return ExecuteNonQuery(connection, statement, parameters)
			End Using
		End Function

		Public Function ExecuteNonQuery(ByVal connection As DbConnection _
		, ByVal statement As String, ByVal parameters() As IDbDataParameter) As Int64

			Using cmd = CreateCommand(connection, statement, parameters)
				Dim result = cmd.ExecuteNonQuery
				If statement.ToLower.Contains("insert into") Then
					Return Convert.ToInt64(ExecuteScalar(connection, _getLastInsertIdStatement, Nothing))
				Else
					Return result
				End If
				Return cmd.ExecuteNonQuery
			End Using
		End Function
#End Region

#Region "GetDataSet"
		'''<summary>Liefert aus dem übergebenen Statement ein Dataset.</summary>
		Public Function GetDataSet(ByVal dr As IDataReader, ByVal tableName As String) As DataSet

			Dim ds = New DataSet
			ds.Load(dr, LoadOption.OverwriteChanges, tableName)
			Return ds
		End Function

		'''<summary>
		'''Liefert aus dem übergebenen Statement ein Dataset.
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		Public Function GetDataSet _
		(ByVal statement As String, ByVal tableName As String) As DataSet

			Return GetDataSet(_defaultConnectionString, statement, tableName, Nothing)
		End Function

		'''<summary>Liefert aus dem übergebenen Statement ein Dataset.</summary>
		Public Function GetDataSet _
		(ByVal connectionString As String _
		, ByVal statement As String _
		, ByVal tableName As String) As DataSet

			Return GetDataSet(connectionString, statement, tableName, Nothing)
		End Function

		''' <summary>
		''' Liefert ein DataTable-Objekt und fügt es dem DataSet-Objekt hinzu.
		''' Ein DataAdapter wird ebenfalls angelegt, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		''' CommandBuilder-Objekt erzeugt. Bereits vorhandene DataTables bleiben im DataSet erhalten.    
		''' Benutzt den hinterlegten DefaultConnectionString.
		''' </summary>
		''' <param name="statement">SQL-Statement</param>
		''' <param name="tableName">Der Name für die erzeugte DataTable</param>
		''' <param name="ds">In dem DataSet werden die Daten zurückgegeben</param>
		''' <param name="da">Beinhaltet den aktiven DataAdapter für die Tabelle [TableName]</param>
		Public Sub GetDataSet _
		(ByVal statement As String _
		, ByVal tableName As String _
		, ByRef ds As DataSet _
		, ByRef da As DbDataAdapter)

			GetDataSet(_defaultConnectionString, statement, tableName, ds, da, Nothing)
		End Sub

		''' <summary>
		''' Liefert ein DataTable-Objekt und fügt es dem DataSet-Objekt hinzu.
		''' Ein DataAdapter wird ebenfalls angelegt, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		''' CommandBuilder-Objekt erzeugt. Bereits vorhandene DataTables bleiben im DataSet erhalten.    
		''' </summary>
		''' <param name="statement">SQL-Statement</param>
		''' <param name="tableName">Der Name für die erzeugte DataTable</param>
		''' <param name="ds">In dem DataSet werden die Daten zurückgegeben</param>
		''' <param name="da">Beinhaltet den aktiven DataAdapter für die Tabelle [TableName]</param>
		Public Sub GetDataSet _
		(ByVal connectionString As String _
		, ByVal statement As String _
		, ByVal tableName As String _
		, ByRef ds As DataSet _
		, ByRef da As DbDataAdapter)

			GetDataSet(connectionString, statement, tableName, ds, da, Nothing)
		End Sub

		'''<summary>
		'''Liefert aus dem übergebenen Statement ein Dataset.
		'''Benutzt den hinterlegten DefaultConnectionString.
		''' </summary>
		Public Function GetDataSet _
		(ByVal statement As String _
		, ByVal tableName As String _
		, ByVal parameters() As IDbDataParameter) As DataSet

			Return GetDataSet(_defaultConnectionString, statement, tableName, parameters)
		End Function

		'''<summary>Liefert aus dem übergebenen Statement ein Dataset.</summary>
		Public Function GetDataSet _
		(ByVal connectionString As String _
		, ByVal statement As String _
		, ByVal tableName As String _
		, ByVal parameters() As IDbDataParameter) As DataSet

			Dim con = CreateConnection(connectionString)
			Dim cmd = CreateCommand(con, statement, parameters)

			Using da = _providerFactory.CreateDataAdapter
				da.SelectCommand = cmd
				Dim ds = New DataSet
				da.Fill(ds, tableName)
				Return ds
			End Using
		End Function

		'''<summary>
		'''Die sub liefert als Ergebnis ein DataTable-Objekt und fügt es dem DataSet-Objekt hinzu.
		'''Ein DataAdapter wird ebenfalls angelegt, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		'''CommandBuilder-Objekt erzeugt. Bereits vorhandene DataTables bleiben im DataSet erhalten.    
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		'''<param name="statement">SQL-Statement</param>
		'''<param name="tableName">Der Name für die erzeugte DataTable</param>
		'''<param name="ds">In dem DataSet werden die Daten zurückgegeben</param>
		'''<param name="da">Beinhaltet den aktiven DataAdapter für die Tabelle [TableName]</param>
		Public Sub GetDataSet _
		(ByVal statement As String _
		, ByVal tableName As String _
		, ByRef ds As DataSet _
		, ByRef da As DbDataAdapter _
		, ByVal parameters() As IDbDataParameter)

			GetDataSet(_defaultConnectionString, statement, tableName, ds, da, parameters)
		End Sub

		''' <summary>
		''' Die sub liefert als Ergebnis ein DataTable-Objekt und fügt es dem DataSet-Objekt hinzu.
		''' Ein DataAdapter wird ebenfalls angelegt, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		''' CommandBuilder-Objekt erzeugt. Bereits vorhandene DataTables bleiben im DataSet erhalten.    
		''' </summary>
		''' <param name="statement">SQL-Statement</param>
		''' <param name="tableName">Der Name für die erzeugte DataTable</param>
		''' <param name="ds">In dem DataSet werden die Daten zurückgegeben</param>
		''' <param name="da">Beinhaltet den aktiven DataAdapter für die Tabelle [TableName]</param>
		Public Sub GetDataSet _
		(ByVal connectionString As String _
		, ByVal statement As String _
		, ByVal tableName As String _
		, ByRef ds As DataSet _
		, ByRef da As DbDataAdapter _
		, ByVal parameters() As IDbDataParameter)

			Dim con = CreateConnection(connectionString)
			Dim cmd = CreateCommand(con, statement, parameters)

			da = _providerFactory.CreateDataAdapter
			da.SelectCommand = cmd
			If ds Is Nothing Then ds = New DataSet
			da.Fill(ds, tableName)
		End Sub
#End Region

#Region "GetDataTable"
		'''<summary>Liefert aus dem übergebenen Statement eine DataTable.</summary>
		Public Function GetDataTable(ByVal dr As IDataReader) As DataTable

			Dim dt = New DataTable
			dt.Load(dr, LoadOption.OverwriteChanges)
			Return dt
		End Function

		'''<summary>
		'''Liefert aus dem übergebenen Statement ein Dataset.
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		Public Function GetDataTable(ByVal statement As String) As DataTable

			Return GetDataTable(_defaultConnectionString, statement, Nothing)
		End Function

		'''<summary>Liefert aus dem übergebenen Statement eine DataTable.</summary>
		Public Function GetDataTable(ByVal connectionString As String, ByVal statement As String) As DataTable

			Return GetDataTable(connectionString, statement, Nothing)
		End Function

		'''<summary>Liefert aus dem übergebenen Statement eine DataTable.</summary>
		Public Function GetDataTable(ByVal connection As DbConnection, ByVal statement As String) As DataTable

			Return GetDataTable(connection, statement, Nothing)
		End Function

		'''<summary>
		'''Liefert aus dem übergebenen Statement eine DataTable.
		'''Benutzt den hinterlegten DefaultConnectionString.
		''' </summary>
		Public Function GetDataTable(ByVal statement As String, ByVal parameters() As IDbDataParameter) As DataTable

			Return GetDataTable(_defaultConnectionString, statement, parameters)
		End Function

		'''<summary>Liefert aus dem übergebenen Statement eine DataTable.</summary>
		Public Function GetDataTable(ByVal connectionString As String _
		, ByVal statement As String, ByVal parameters() As IDbDataParameter) As DataTable

			Using connection = CreateConnection(connectionString)
				Using cmd = CreateCommand(connection, statement, parameters)
					Return CreateDataTableByDataAdapter(cmd)
				End Using
			End Using
		End Function

		'''<summary>Liefert aus dem übergebenen Statement eine DataTable.</summary>
		Public Function GetDataTable _
		(ByVal connection As DbConnection _
		, ByVal statement As String _
		, ByVal parameters() As IDbDataParameter) As DataTable

			Using cmd = CreateCommand(connection, statement, parameters)
				Return CreateDataTableByDataAdapter(cmd)
			End Using
		End Function

		'''<summary>
		'''Liefert ein DataTable-Objekt, welches die abgefragten Daten enthält
		'''und einen DataAdapter, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		'''CommandBuilder-Objekt erzeugt.    
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		'''<param name="statement">SQL-Statement</param>
		'''<param name="dt">In der DataTable werden die Daten zurückgegeben</param>
		'''<param name="da">Beinhaltet den aktiven DataAdapter</param>
		Public Sub GetDataTable _
		(ByVal statement As String, ByRef dt As DataTable, ByRef da As DbDataAdapter)

			GetDataTable(_defaultConnectionString, statement, dt, da, Nothing)
		End Sub

		''' <summary>
		''' Liefert ein DataTable-Objekt, welches die abgefragten Daten enthält
		''' und einen DataAdapter, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		''' CommandBuilder-Objekt erzeugt.    
		''' </summary>
		''' <param name="statement">SQL-Statement</param>
		''' <param name="dt">In der DataTable werden die Daten zurückgegeben</param>
		''' <param name="da">Beinhaltet den aktiven DataAdapter</param>
		Public Sub GetDataTable _
		(ByVal connectionString As String, ByVal statement As String _
		, ByRef dt As DataTable, ByRef da As DbDataAdapter)

			GetDataTable(connectionString, statement, dt, da, Nothing)
		End Sub

		'''<summary>
		'''Liefert ein DataTable-Objekt, welches die abgefragten Daten enthält
		'''und einen DataAdapter, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		'''CommandBuilder-Objekt erzeugt.    
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		'''<param name="statement">SQL-Statement</param>
		'''<param name="dt">In der DataTable werden die Daten zurückgegeben</param>
		'''<param name="da">Beinhaltet den aktiven DataAdapter</param>
		Public Sub GetDataTable _
		(ByVal statement As String _
		, ByRef dt As DataTable _
		, ByRef da As DbDataAdapter _
		, ByVal parameters() As IDbDataParameter)

			GetDataTable(_defaultConnectionString, statement, dt, da, parameters)
		End Sub

		'''<summary>
		'''Liefert ein DataTable-Objekt, welches die abgefragten Daten enthält
		'''und einen DataAdapter, welcher zur Datenmanipulation benutzt werden kann. Zudem wird automatisch ein
		'''CommandBuilder-Objekt erzeugt.    
		'''</summary>
		'''<param name="statement">SQL-Statement</param>
		'''<param name="dt">In der DataTable werden die Daten zurückgegeben</param>
		'''<param name="da">Beinhaltet den aktiven DataAdapter</param>
		Public Sub GetDataTable _
		(ByVal connectionString As String _
		, ByVal statement As String _
		, ByRef dt As DataTable _
		, ByRef da As DbDataAdapter _
		, ByVal parameters() As IDbDataParameter)

			Using con = CreateConnection(connectionString)
				Using cmd = CreateCommand(con, statement, parameters)
					da = _providerFactory.CreateDataAdapter
					da.SelectCommand = cmd
					dt = New DataTable
					da.Fill(dt)
				End Using
			End Using
		End Sub
#End Region

#Region "Transaction"
		'''<summary>
		'''Beginnt eine Transaktion.
		'''Benutzt den hinterlegten DefaultConnectionString.
		'''</summary>
		Public Sub BeginTransaction()
			Try
				_con = CreateConnection(_defaultConnectionString)
				_trans = _con.BeginTransaction(IsolationLevel.ReadCommitted)
			Catch ex As Exception
				If Not _con Is Nothing Then _con.Dispose()
				If Not _trans Is Nothing Then _trans.Dispose()
				Throw
			End Try
		End Sub

		'''<summary>Beginnt eine Transaktion.</summary>
		Public Sub BeginTransaction(ByVal connectionString As String)
			Try
				_con = CreateConnection(connectionString)
				_trans = _con.BeginTransaction(IsolationLevel.ReadCommitted)
			Catch ex As Exception
				If Not _con Is Nothing Then _con.Dispose()
				If Not _trans Is Nothing Then _trans.Dispose()
				Throw
			End Try
		End Sub

		'''<summary>Beendet eine Transaktion mit Übernahme der Änderungen.</summary>
		Public Sub EndTransactionCommit()
			Try
				_trans.Commit()
			Catch ex As Exception
				Throw
			Finally
				_trans.Dispose()
				_con.Dispose()
			End Try
		End Sub

		'''<summary>Beendet eine Transaktion mit Verwerfung der Änderungen.</summary>
		Public Sub EndTransactionRollback()

			Try
				_trans.Rollback()
			Catch ex As Exception
				Throw
			Finally
				_trans.Dispose()
				_con.Dispose()
			End Try
		End Sub

		'''<summary>Führt eine Transaktionsabfrage aus ohne Rückgabe.</summary>
		Public Function ExecuteTransactionNonQuery _
		(ByVal statement As String, ByVal parameters() As IDbDataParameter) As Int64

			Try
				Using cmd = CreateCommand(_con, statement, parameters)
					cmd.Transaction = _trans
					Return cmd.ExecuteNonQuery()
				End Using
			Catch ex As Exception
				EndTransactionRollback()
				Throw
			End Try
		End Function

		'''<summary>Führt eine Transaktionsabfrage aus ohne Rückgabe.</summary>
		Public Function ExecuteTransactionNonQuery(ByVal statement As String) As Int64

			Return ExecuteTransactionNonQuery(statement, Nothing)
		End Function
#End Region

#End Region

	End Class

End Namespace