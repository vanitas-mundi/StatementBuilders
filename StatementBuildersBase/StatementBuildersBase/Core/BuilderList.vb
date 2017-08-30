Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Base
#End Region

Namespace Core

  Public Class BuilderList

    Inherits List(Of String)

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
    Private _delimiter As String
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
    Public Sub New(ByVal delimiter As String)
      _delimiter = delimiter
    End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
    Public ReadOnly Property Delimiter As String
      Get
        Return _delimiter
      End Get
    End Property
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
    Public Overloads Sub Add(ByVal format As String, ByVal ParamArray args() As Object)
      MyBase.Add(Helper.String.Format.GetStringFormat(format, args))
    End Sub

    Public Overloads Sub Add(ByVal item As String)
      MyBase.Add(item)
    End Sub

#End Region

  End Class

End Namespace

