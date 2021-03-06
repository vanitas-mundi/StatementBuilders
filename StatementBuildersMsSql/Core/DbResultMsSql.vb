﻿Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core
Imports System.Data.SqlClient
Imports SSP.Base
#End Region

Namespace Core

  Public Class DbResultMsSql

    Private Shared _instance As DbResultBase
    Private Shared _defaultConnectionString As String
    Private Shared _quotingRules As QuotingRulesMsSql

#Region " --------------->> Enumerationen der Klasse "
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
    Shared Sub New()

      _quotingRules = New QuotingRulesMsSql
    End Sub
    Private Sub New()
    End Sub

    Public Shared Sub Initialize()

      Initialize("")
    End Sub

    '''<summary>Initialisiert das DbResult-Objekt mit einem Standard-ConnectionString.</summary>
    Public Shared Sub Initialize(ByVal defaultConnectionString As String)

      _defaultConnectionString = defaultConnectionString
      _instance = DbResultBase.CreateDbResultBaseObject _
      (SqlClientFactory.Instance, defaultConnectionString, My.Settings.GetLastInsertIdStatementMsSql)
    End Sub
#End Region '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
    Public Shared ReadOnly Property Instance As DbResultBase
      Get
        Return _instance
      End Get
    End Property

    Public Shared ReadOnly Property DefaultConnectionString As String
      Get
        Return _defaultConnectionString
      End Get
    End Property
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
    '''<summary>Liefert ein QuotingRules-Objekt mit Parameter-Quotierungs-Regeln.</summary>
    Public Shared Function QuotingRules() As QuotingRulesMsSql
      Return _quotingRules
    End Function

    '''<summary>Maskiert einen String für eine MySQL-Abfrage.</summary>
    Public Shared Function ReplaceEscape(ByVal s As String) As String

      Return Helper.String.Replace.EscapeMsSql(s)
    End Function
#End Region '{Öffentliche Methoden der Klasse}

  End Class

End Namespace
