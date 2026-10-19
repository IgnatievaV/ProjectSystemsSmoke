Imports System
Imports Smoke.Library

Module Program
    Sub Main(args As String())
        Console.WriteLine(Greeter.Hello("VB"))
        Console.WriteLine(New Actions.Rename.RenameMe().Name)
    End Sub
End Module
