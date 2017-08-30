Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports "
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class DeleteBuilderAD

		Inherits DeleteBuilderBase

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
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Lösch-Statement unter Verwendung der Default-Connection aus.</summary>
		Public Overrides Function ExecuteNonQuery() As Int64
			Return ExecuteNonQuery(DbResultAD.DefaultConnectionString)
		End Function

		'''<summary>Führt das Lösch-Statement aus.</summary>
		Public Overrides Function ExecuteNonQuery(connectionString As String) As Int64
			Return DbResultAD.Instance.ExecuteNonQuery(connectionString, Me.ToString)
		End Function
#End Region '{Öffentliche Methoden der Klasse}

	End Class

End Namespace






