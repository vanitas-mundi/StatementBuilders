Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports "
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class SelectBuilderAD

		Inherits SelectBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New(Nothing)
		End Sub
#End Region  '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
		'''<summary>Angabe des DistinguishedNames in folgendem Format: 'LDAP://OU=test,DC=test-intern,DC=local'</summary>
		Public Shadows ReadOnly Property From As BuilderList
			Get
				Return MyBase.From
			End Get
		End Property
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Select-Statement unter Verwendung des Default-ConnectionStrings aus.</summary>
		Public Overrides Function ExecuteReader() As IDataReader
			Return ExecuteReader(DbResultAD.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteReader(connectionString As String) As IDataReader
			Return DbResultAD.Instance.ExecuteReader(connectionString, Me.ToString)
		End Function

		'''<summary>Führt das Select-Statement unter Verwendung des Default-ConnectionStrings aus.</summary>
		Public Overrides Function ExecuteScalar() As Object
			Return ExecuteScalar(DbResultAD.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overloads Overrides Function ExecuteScalar(ByVal connectionString As String) As Object
			Return DbResultAD.Instance.ExecuteScalar(connectionString, Me.ToString)
		End Function

		'''<summary>Führt das Select-Statement unter Verwendung des Default-ConnectionStrings aus.</summary>
		Public Overrides Function ExecuteStringScalar() As String
			Return ExecuteStringScalar(DbResultAD.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overloads Overrides Function ExecuteStringScalar(ByVal connectionString As String) As String
			Return DbResultAD.Instance.ExecuteStringScalar(connectionString, Me.ToString)
		End Function

		Public Function GetFieldList(Of T)(ByVal field As String) As List(Of T)

			Return GetFieldList(Of T)(DbResultAD.DefaultConnectionString, field)
		End Function

		Public Function GetFieldList(Of T)(ByVal connectionString As String, ByVal field As String) As List(Of T)
			Return DbResultAD.Instance.GetFieldList(Of T)(connectionString, Me.ToString, field)
		End Function
#End Region  '{Öffentliche Methoden der Klasse}

	End Class

End Namespace






