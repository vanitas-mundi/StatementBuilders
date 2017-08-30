
Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core.Interfaces

	Public Interface IValueQuotingRules
		Function GetQuotedValue(Of TValue As IConvertible)(ByVal value As TValue) As String
	End Interface

End Namespace