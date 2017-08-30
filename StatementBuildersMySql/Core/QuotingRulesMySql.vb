Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Base
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

  Public Class QuotingRulesMySql

    Implements IValueQuotingRules

#Region " --------------->> Enumerationen der Klasse "
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
#End Region '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
    Public Function GetQuotedValue(Of TValue As IConvertible) _
    (value As TValue) As String Implements IValueQuotingRules.GetQuotedValue

      Select Case True
        Case value.GetType Is GetType(String), value.GetType Is GetType(Char)
          Return String.Format("'{0}'", Helper.String.Replace.EscapeMySql(Convert.ToString(value)))
        Case value.GetType Is GetType(Int32), value.GetType Is GetType(Int64), value.GetType Is GetType(Int16) _
        , value.GetType Is GetType(UInt16), value.GetType Is GetType(UInt32), value.GetType Is GetType(UInt64) _
        , value.GetType Is GetType(Byte), value.GetType Is GetType(SByte)
          Return Convert.ToString(value)
        Case value.GetType Is GetType(DateTime)
          Return String.Format("'{0}'", Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"))
        Case value.GetType Is GetType(Decimal), value.GetType Is GetType(Double), value.GetType Is GetType(Single)
          Return Helper.String.Replace.CommaToPoint(Convert.ToString(value))
        Case value.GetType Is GetType(Boolean)
          Return If(Not Convert.ToBoolean(value), "0", "1")
        Case value.GetType Is GetType(DBNull)
          Return "NULL"
        Case Else
          Throw New FormatException(String.Format("Invalid DataType '{0}'", value.GetType))
      End Select
    End Function
#End Region '{Öffentliche Methoden der Klasse}

  End Class

End Namespace


