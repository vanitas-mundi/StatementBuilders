
Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Enums
#End Region

Namespace Core.Interfaces

	Public Interface IBuilderBase
		Function GetStatement() As String
		Function GetStatement(ByVal statementFormat As StatementFormats) As String
		Function ToString() As String
		Function ToString(ByVal statementFormat As StatementFormats) As String
	End Interface

End Namespace