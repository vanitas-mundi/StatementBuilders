Option Explicit On
Option Strict On
Option Infer On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core

	Public Class Parameters

		Inherits List(Of Parameter)

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
		Public Overloads Sub Add _
		(ByVal name As String, ByVal value As Object)

			Me.Add(New Parameter(name, value))
		End Sub

		Public Function GetIDbParameters _
		(Of T As IDbDataParameter)() As T()

			Dim ret = From p In Me
								Select DirectCast(GetType(T).InvokeMember(Nothing _
			, Reflection.BindingFlags.DeclaredOnly _
			Or Reflection.BindingFlags.Public _
			Or Reflection.BindingFlags.NonPublic _
			Or Reflection.BindingFlags.Instance _
			Or Reflection.BindingFlags.CreateInstance, Nothing, Nothing _
			, New Object() {p.Name, p.Value}), T)

			Return ret.ToArray
		End Function
#End Region

	End Class

End Namespace