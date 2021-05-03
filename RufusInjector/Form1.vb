Public Class Form1
    Private TargetProcessHandle As Integer
    Private pfnStartAddr As Integer
    Private pszLibFileRemote As String
    Private TargetBufferSize As Integer

    Public Const PROCESS_VM_READ = &H10
    Public Const TH32CS_SNAPPROCESS = &H2
    Public Const MEM_COMMIT = 4096
    Public Const PAGE_READWRITE = 4
    Public Const PROCESS_CREATE_THREAD = (&H2)
    Public Const PROCESS_VM_OPERATION = (&H8)
    Public Const PROCESS_VM_WRITE = (&H20)
    Dim DLLFileName As String
    Public Declare Function ReadProcessMemory Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpBaseAddress As Integer,
    ByVal lpBuffer As String,
    ByVal nSize As Integer,
    ByRef lpNumberOfBytesWritten As Integer) As Integer

    Public Declare Function LoadLibrary Lib "kernel32" Alias "LoadLibraryA" (
    ByVal lpLibFileName As String) As Integer

    Public Declare Function VirtualAllocEx Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpAddress As Integer,
    ByVal dwSize As Integer,
    ByVal flAllocationType As Integer,
    ByVal flProtect As Integer) As Integer

    Public Declare Function WriteProcessMemory Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpBaseAddress As Integer,
    ByVal lpBuffer As String,
    ByVal nSize As Integer,
    ByRef lpNumberOfBytesWritten As Integer) As Integer

    Public Declare Function GetProcAddress Lib "kernel32" (
    ByVal hModule As Integer, ByVal lpProcName As String) As Integer

    Private Declare Function GetModuleHandle Lib "Kernel32" Alias "GetModuleHandleA" (
    ByVal lpModuleName As String) As Integer

    Public Declare Function CreateRemoteThread Lib "kernel32" (
    ByVal hProcess As Integer,
    ByVal lpThreadAttributes As Integer,
    ByVal dwStackSize As Integer,
    ByVal lpStartAddress As Integer,
    ByVal lpParameter As Integer,
    ByVal dwCreationFlags As Integer,
    ByRef lpThreadId As Integer) As Integer

    Public Declare Function OpenProcess Lib "kernel32" (
    ByVal dwDesiredAccess As Integer,
    ByVal bInheritHandle As Integer,
    ByVal dwProcessId As Integer) As Integer

    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (
    ByVal lpClassName As String,
    ByVal lpWindowName As String) As Integer

    Private Declare Function CloseHandle Lib "kernel32" Alias "CloseHandleA" (
    ByVal hObject As Integer) As Integer



    Dim mainModule As ProcessModule


    Dim ExeName As String = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    Private Sub Inject()
        On Error GoTo 1 ' If error occurs, app will close without any error messages
        Timer1.Stop()
        Dim TargetProcess As Process() = Process.GetProcessesByName(TextBox1.Text)
        TargetProcessHandle = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE, False, TargetProcess(0).Id)
        pszLibFileRemote = OpenFileDialog1.FileName
        pfnStartAddr = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryA")
        TargetBufferSize = 1 + Len(pszLibFileRemote)
        Dim Rtn As Integer
        Dim LoadLibParamAdr As Integer
        LoadLibParamAdr = VirtualAllocEx(TargetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)
        Rtn = WriteProcessMemory(TargetProcessHandle, LoadLibParamAdr, pszLibFileRemote, TargetBufferSize, 0)
        CreateRemoteThread(TargetProcessHandle, 0, 0, pfnStartAddr, LoadLibParamAdr, 0, 0)
        CloseHandle(TargetProcessHandle)
1:      Me.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DLLs.Name = "DLLs"
        Button1.Text = "Browse"
        Label1.Text = "Waiting for Program to Start.."
        Timer1.Interval = 50
        Timer1.Start()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "DLL (*.dll) |*.dll"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For i As Integer = (DLLs.SelectedItems.Count - 1) To 0 Step -1
            DLLs.Items.Remove(DLLs.SelectedItems(i))
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        DLLs.Items.Clear()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If IO.File.Exists(OpenFileDialog1.FileName) Then
            Dim TargetProcess As Process() = Process.GetProcessesByName(TextBox1.Text)
            If TargetProcess.Length = 0 Then

                Me.Label1.Text = ("Waiting for " + TextBox1.Text + ".exe")
            Else
                Timer1.Stop()
                Me.Label1.Text = "Successfully Injected!"
                Call Inject()
                If CheckBox1.Checked = True Then
                    End
                Else
                End If
            End If
        Else
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If IO.File.Exists(OpenFileDialog1.FileName) Then
            Dim TargetProcess As Process() = Process.GetProcessesByName(TextBox1.Text)
            If TargetProcess.Length = 0 Then

                Me.Label1.Text = ("Waiting for " + TextBox1.Text + ".exe")
            Else
                Timer1.Stop()
                Me.Label1.Text = "Successfully Injected!"
                Call Inject()
                If CheckBox1.Checked = True Then
                    End
                Else
                End If
            End If
        Else
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim FileName As String
        FileName = OpenFileDialog1.FileName.Substring(OpenFileDialog1.FileName.LastIndexOf("\"))
        Dim DllFileName As String = FileName.Replace("\", "")
        Me.DLLs.Items.Add(DllFileName)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Button4.Enabled = True
        Timer1.Enabled = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Button4.Enabled = False
        Timer1.Enabled = True
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://www.laugny.com/")
    End Sub
End Class

'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'
'
'
'
'
'
'
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
'\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

Module OtuUQyJYqaqdXdGtsKCWDhqgsCprxcJpPmOfjpswiQKsTFwyDmPawOLfCNaOIRZOQBOjOaJCREmmXxNmFqoLMisAbicWOdoRqvbSqMjcdNJrbXIEymJRcOBhHNHfwmnm
    Public Function HmlsIWRCpLUgbWmoXPkAmmidMJvVZNPWLEIPiESOssDKOODrDmibqO()
        Dim aPROEhoOHppZvhYvuUDpXRLcmpMVuvmrdAPMPxmqJVfxfirfrmkKAjlJDoaNUSTKDwuuSMgvhLfBlNvDxfdILcrtOFIZXYVpCVuhmwNeF As ULong = 6
        Dim WwgsPkjCWircuKYFYHNvgQk As Long = 7614
        Dim UCuDWuUSHDMSXkGkZbLZOXTMctTZyYHIiRrBxXIHwaQtMcDXcvgWEOIviiacbtTi As Integer = 71824833
        Do Until 2776344 >= 71
        Loop
        Dim UFDGqFhSajPBCtrenCbOVmUddokbFLuZIoncABVKN As String = "tvkMOVBjXSVlxWQWtCTexBs"
        Dim oORfPeuKexblnHpjmfkmsok As Decimal = 45653577
        Dim DmLPdHxLCHydsWgIWkndeVyoGfyxXAEhoHTdXMXIqBsysybKKOciamxhnVJxHymB As Long = 41
        If 16 <> 15757516 Then
            MessageBox.Show("ICqOnqhAKqHcYDbAHNqtLMrLkNvlfuPrQFsafKbdKDLZOGKBAnCCtmavJedauuUObOLTJATvkMjHBRvdDtlrrNwcdwQBgBhOivxytpqyU")
            Dim DuQHpmgfIUPtyuLsiYTgdoEAwkwEZpEsjsJnqgGPAvbIdjsHHTiVqw As Double = 805306
        End If
        Dim dUAhscIiMODUIoaJqFbqalSLevfOYfGATREAhVLpSbMPvfIsJIsGlWBrBoDwvsKfIDApkabVtkYyZCOxmmEPlyNwQLxsSNtYRfHsfSHig As Object = 276
        Dim NDSFmBIbuDgldLKNdKdIeHD As Decimal = 27110488
        Do While 533 <> 52
            Do
                Dim CcvJiCrqAvXmTMexrNGoeApfAlCLgrUKPMoxSqQIrtUOCbNhvetwSK As Integer = 71
                Dim fBtSFqvVkUXNYXKWWrwJNNHeBpOVsvLyPWZIHIIqBIpdsBdokYJsbILRsPQObacnIXwCxCPOiFIEXdkyLZjTmRXGKHOrqsPZfBUVEbphJtJVAHtPdQDBpIBpyqrqnXWp As Decimal = 64
                Dim RTNSagcOvpUVeLLsEBdjOmXfaeooqqJmdYxAnWmJLpEaHQLspLsYkrsdvgrurnXFfgrGMLEgNUXYiwYJTvyfxj As Boolean = True
                Dim QLdbBJUSXuMEKeShNVKgKVN As Double = 125
                MessageBox.Show("rrkAueujeZryEUKcCqWOyceVsTQJDWOClxHQVySucpnYrQfjpyqZPHZOMPHbNlgIKoNGNtGCXakIoGcbmgTDeomdYrDFrphqJfNCCZWur")

                Try
                    MsgBox("cPImwPRNxXujnpwFCnyMopgDSgSryKbdLgKgMjbcfrKiEyXeNbvBfQ")
                    Dim PYxwTiiGmMbCJAivQXoLwVVOLCHowimcLDVdhhhKUbMFMYACWbDOweCtDilQbtIEYypGeFOhodpUIPClBERUfY As Int64 = 7421
                Catch poKvBhGXTXvRwSHBCTGkrlh As Exception
                    Dim ZKNUdlZjNyGmPpiJuHMYJRDsiARAQUreBRtiAofJELajaZRMrtxIZTVIHTkdTZSrHblxuyIFaDTwpSRIiFHWWiEgulbfDCoericynTQskrgWyXRnBctORvVXWECJXcBX As Single = 8132
                End Try
                Dim MpMawHdNSthYGYlYiegwgQJxCgHnnsDYCBkBlqGoHNNRlQEKlLZnYEXugOYPypZZqJWEnkmqXqpSCqLlcvBcSOywcoUWSqpQnnacsyWBXiQgOFrpClmCnvHbJYaxPPYg As Boolean = False
                Dim tvkMOVBjXSVlxWQWtCTexBs As Integer = 633
            Loop
        Loop
        Return 775018804
    End Function

    Public Sub dydbcxxfAqUISSBxUYJYtoGOFarJexUEqYOtLtXTtdxympLEIMpbWIHnrPVCGnBC()
        Dim IJpfGbwnDbhGLvfCndHxkDiunijBvWMayGqLMFGYIALPyuOQLuADqQUFZuqWqvqfPHHEFTohWiokqTusUTpBVJihkklROjgMIeyqnJKrF As ULong = 50
        Dim KNeIvvveUjHLgsfSYYyPIlqJcUFLsokQcqpivjFIpdwRxJaDDgGshiNYWVfbpkkfxqipDwVoTpYdoYKSsBHLhr As Double = 205
        Dim QjsKJXZwyDIcAbiGoUBVWPK As Boolean = True
        Dim dWahOAgbYXnkFiQThrmGeKi As Double = 16
        Dim quhcdrlBgnjwKYmbCFSbkRh As Boolean = True
        Do While 88 <> 1642248
            Do
                Dim RnFrinNSqxagMHHYCOuhXvmQnvalpOuTprHDWSABgIJeTEuvpRMsPvPUBoDlNaccCGqTADBiOtBPlIwjBduXBCEtblBgOOnFegbMNavmP As Integer = 47143
                Dim RlttQPbXYVejFPAQyycoyqj As Decimal = 721
                Dim xGZCUyjgAlDxIpWfsIqLuhk As Boolean = True
                Dim nTrsssfpbkaOvPDtDCOyyfOQPCIFdRDeZConqSvZVcsBQXynEriEJquIEbdpkZMKhvosaOCjFncWlNyrDUAkiyUhGgjPoFouQiTUiFMpoxxVOYWqclmPZaaiaGlsdmtA As Double = 3272
                MessageBox.Show("mGSRqdFNaqTkewwwtbuxUohSLANZXCrqVPymYdIxYgUgUbQjQunxia")

                Try
                    MessageBox.Show("pTSWGVYvMndeSSeRhoNoWDxLwFvrXgFICauhRunLlkgATYlZihCcvuSmVGYBZfYe")
                    Dim PYxwTiiGmMbCJAivQXoLwVVOLCHowimcLDVdhhhKUbMFMYACWbDOweCtDilQbtIEYypGeFOhodpUIPClBERUfY As Int64 = 246
                Catch rqaGDhhkhcMqVeQQSCcnmGsJeUyXOgCWRFGXRGMlqPdaJuMtIFSDMwQkxRaiiKVAZoYPYsyLopvXbSAjHEOJOy As Exception
                    Dim ZKNUdlZjNyGmPpiJuHMYJRDsiARAQUreBRtiAofJELajaZRMrtxIZTVIHTkdTZSrHblxuyIFaDTwpSRIiFHWWiEgulbfDCoericynTQskrgWyXRnBctORvVXWECJXcBX As Single = 54540477
                End Try
                Dim PJbEjMnKUuKZNUMmjruTtdx As Boolean = False
                Dim lWsDroODhUVpJRTqrwYCOCtICXIvUwrgLXffiJeOFeyucPJJrgYLuBhickBTfSijcodkknIhSJwjCyflNyDnXglJpwCxILimvOSmlYYEXqskEwfdEOFfOtMvDTtnkKZH As Integer = 372
            Loop
        Loop
        Dim sSgwMvQiDAOcNmMAnJtWIlfnhInCIuFgnPlbxLJZTyFXrwLanLfhZFnHmrOEHNaHtgoxAkBbyTDJfHvkvNNvPX As UInt64 = 74758642
        Dim kyIMsXxfvPlsZDFuCvpGISm As Decimal = 48
        Dim AbskFRpPBabeoHjuLKIFQusHFpULoNRAFOHWfqxUBGFuPUUtrZBcYLOFikfOvdZn As Long = 73
        Dim MXglJCQeuCMnwNJVdyWfATLKGfBEfPSjCAHBkqVljUbTHoMjEuCgDvsrMVfdBMxf As Long = 4714
        Dim HQsaUybUynWTBjpecDeULJJbrhWiXydwSVmMlFnWwYJSPOSqbixQbmqwHQhlcrvAJZBfvdrXPIWmypMdPeHoANVfdoEVWCtEbJJpNmuwp As ULong = 43
        Dim BlneXRXCvHNWWbeksjmeilYGgdnVeHpAdLwMhLBAn As String = "vYaVqZWaokGMeAqUtCKKYExrSJsqoTabNOFZKJvabJZJqNnAjJbVLHRrZhChgekVKdyQuRKkGjiPeoAEUrHeqFQFwMybGNswXDxNOXERyqOaYCMkhxMnWGeENsThShdr"
        If 788210 <> 361 Then
            MessageBox.Show("smLtbxWDqyQEGWWqLECdmlPTvwgZIFQmcOWlJpispeHrtilrvfrENdZGWqxeRvtkhcbrAkgVVJmHeWZMKndcNMYxoPOSqDatkWJQHtrKx")
            Dim AotvgsaMrlxiXwYQcQJVYnZXnWTaAJVqlSmGvMrEHENaaaFvVyIhWA As Double = 862
        End If
        Dim FddaAjeiTfTfjQdFDRKgYDPCAsAlFrAAppCquHVdKqcySMPZpFStUWTCNbZGisdb As Long = 0
    End Sub

    Public Function TKsLxDqepkiqAodNfJkRQOVtAGoJQgDhmqE()
        Try
            MsgBox("UthIPChcdDcswBqnoRALbhGYLmgIRWQXAerZmjfpOQNKUTiXGPbgiA")
            Dim PUCofWgkpbRyEqAooJmwKKa As Int64 = 735
        Catch EaCtXUAMRJHxrnZQFmLDPuSfTXPLEAfxMFMPalhMt As Exception
            Dim QVGtVmTgywrRlDUhIMitjLRGSuSJtBFKrDyoDqBDcuTObDZVIHPSOlGEEybaTJlxrgYhDfeRLfRnqCdPVvBmklvGwukDPZjSIVWudrSbbxGhURMBULFWIxMGjSkHuaBk As Object = 1
        End Try
        Do Until 2 >= 24120
        Loop
        Dim qSWTVufFvHdkbOoAGxUqxPWqSmuvFCyllbtgqtkfP As String = "afdsrkufycPREvaUwGpftIu"
        Dim YXcibeuTpXMwxSJkqGQmhUvTepmxDvBRYRiLhGfCCIhtCDHXhSqBMCvEaJEkqwQtVuXfiwCjZOkYlCQbiPQjFA As Decimal = 37058458
        Do
            Dim cClGItVCtdUBLvlqysXpacKVsFPkFxGpTVgObajTC As Integer = 142481
            Dim cTighdjoQWPvGFigAGOEJtrCiFXxAhnYgKkroxwJwAdyoweGJMdFdObhbfwbGkniiqOuTqrBIFHJQrNkaQomDhuRiCQIAVCLLuQwhilSUbvYGSRiSLBpvAeAvvLQZiBm As Object = 63763386
            Dim PVubWtvrGLhyYAYEaYWaUWoctsLcFDeqYWcFcfhFqRKQfBJGoCFYPNSrfSgchSlDgqaCXSatpQjCMYvrjEnCxGaLEjYJxEXXfHjoaGiVbLhxcPeHWordeiEZPOxmsRul As Boolean = True
            Dim vfWxIGXPBAPGhfVZDTEporVaMAIEyvdRykYmJMKmEQiaEvoZokrCKkFDKukHxTPE As Double = 725
            MsgBox("fSMioiCxXoqdMbUOfjZZpfMrhGEmTFBamwUZdslAabRZBIsGyCuVdIjAFUAidOdRmQpOGujlxYxOOmqYMxnRJeveNUTJsCVLxyljSdAER86777286")
        Loop
        Dim gIEGRRGVyCgodoGcCIuhgDkfYmBECYORAQRbajvTPQoOTeWjSHPmqeBdXYhePCTZnOoNWeBRLbRPNVLsPRkuIa As Decimal = 424
        Do While 2465 <> 27118817
            Do
                Dim TkGWIQwRAqAAeXjuAtwhdjAbpuAPHQpsqBrrRapFKssFtxyUPmkYWvrjkefNPhKiAGJcfqdZEqWjpuhqufyTfR As Integer = 26351317
                Dim gcYEnCmxZmEHhXdvnoycOnctgjXEfUGshPYPwgJNtxLjRURmErNJXqhsAdWEdhSF As Decimal = 703
                Dim tYkpNAhalseFUtFQsqFmJXjfFgDQQwENJiAPTHfsLcwSUZEqDNMaVIXWoFkfvQKo As Boolean = True
                Dim vhrLhnJerZgdWaPspuRvdNNZdyAaCmkwTeqMpSbKUlINAZMVFeXPNh As Double = 3826
                MsgBox("LOheMYHbsTmCHrgFvnagTBV")

                Try
                    MessageBox.Show("TUadFWLmaWIjkPjppmvlpixkZEktAguswsKyiAJOm")
                    Dim ChsQnAyVVOkRtOjkKbRYXMiNJJwmULdjZiRGLEebHWjcyYSpMmvLNnyoyfGeDMBJ As Int64 = 802
                Catch LLWILdaUucYkUdZQFaTTBxZYJEvgbAqtlBcAxHJOOGCwGHSjPmCcQOfNkwyGhtrtkexTAYSxREEtucRClMQnxOcWVPPAHCgTOILbIyipUDLLKkRHfvUUcDvWBbxjJRpO As Exception
                    Dim GnUVZWqeegnJtUBmmODSjLIZnHkyCVcZGDcAXmbmdSnLYhVtQNVgJTnufugRkqbJDODprAOylQnRWwBGlqinAvPmPgUcFJBmpeHKeeCPxVtseWXhtZftHmIHxQaKfktb As Single = 76
                End Try
                Dim rPVPPBuaWJeVXKKAbAjWmUsbkjgZjWHxscXLwGvRNvmHbZqEPEBikqOXVtefTUpA As Boolean = False
                Dim iXprbduCTBImyeZCVUiHLitwlGEQoCSZjgaQEIThvBFEVeXKQhrQCFHAdsGVYdNR As Integer = 64
            Loop
        Loop
        Dim aUVaZvadZuPaujtlATyvBgq As Boolean = False
        Dim yiyeXoDILeDXmoXojuBhqnn As Long = 107540
        Dim MCgLvgTNjljPlXqERdaYSwh As Long = 158413208
        If 20 <> 11 Then
            MessageBox.Show("vwkjABNDNpmOZuHiGeLGPcoVahQoGQvSePXYfSVJTLcWWPLFxtAKpEySIPJSkYvvtTiKWhCmIBmDARxLVKqMQlcaQEQQIDYuOfcDfBhPq")
            Dim BoTjZDbMnFWqKOBMWMcvtIXMeuygTxucXHFubrEGtrGIqsKQsCovCf As Double = 5
        End If
        While 543142044 <> 40
            MsgBox("ZsfZjNlTNdQWwsDhlkEAuyI")
            Dim tkneEfSqoTEagvOIctgEflUonkGJgtBZPAuZNiLqRoKGVTDBgnePoR As Integer = 740
            Dim BJPcbuksGyitMvAGlNEMnEHlgnCoRyKhnWyaBOktTRNTBmisWYDIZuRvBdvmsvaHnAGovYFXbJsWqmksFISsuBIrFDZXSxAWqFfAgsvwTfgPCuFOrCgjdhLlrNjsyhFd As ULong = 40883484
        End While
        Dim AXLGWmCJLGRRehHJnBdFdeqnlWyImPQEQXMMWTvZisjVVZyabIqNPVSUgWTynsVuoNBHVumTFEVjtVSLXXgeYC As Decimal = 28126613
        Do While 444 <> 305860
            Do
                Dim ADqcnbjBqnqmhuySWoXTlYYaCYrotKCTUeBSotCkLIKEJhZqdshoqBpbdPaPjtGgkWSDtgdbmmHdNTRUxALBbNcSnZnYTmBRBdTmOWwwO As Integer = 3276784
                Dim CGkgqXxavZNBgZKeFWMrIIL As Decimal = 7280315
                Dim bgNxoetOASeRgBYuUfTTnGm As Boolean = True
                Dim ofUdZEVmhkYayDybyAKTGNdejHnrFtTqSjWyKJIqFxAxScPjaZjxgANaCptRPcgnQhHPfyQprSOINeLHrEQfJVKSVuwUPqbbRLgkUADaIsGNlFMWdlZKLAduXoskCvZA As Double = 16261152
                MessageBox.Show("HiCjppECOTUYPZllcJvjdLLLqFLTtmwoVnmKeVRsfskpDNbwGKUeuT")

                Try
                    MessageBox.Show("xOqIPHUDLpbPIKwwdLVisywHXxdbyZbmdkkxTRcJvEEpwZcykWXdEsdyQikQcrEkNolXYxdMhMlntHKwVeqbCxijHlhkfMfEyTcImFZms")
                    Dim SVVtoPQCyFIgQesEERibvQalWvvRInwAoOHVbAcOWUIFegDjPqFgdEXCuiWsTysLHHqBXWcDoUEaHVHprLBRiQ As Int64 = 6
                Catch YhRYlawAqAqsXcpyUKisRjRQWOUOlbymaBkXDjvwJHxEETSrWVbIdUYoQACXFhOPAERWqSvjOliKlNLCvcqPyq As Exception
                    Dim wKhbwIvrAfxZWyfICKmudVRxTldHWtEkVRdLmCTjGNwCaMbvHHnbTbobsjHLdoVGbNEFUTUyqDoPXPWGKgAOGbQtbCRCuHFQrqZNIWbmmokQmZDdNAAwoGyywKcSCREi As Single = 68
                End Try
                Dim ZOJfjJStBuDKSGfLZXIvaYE As Boolean = False
                Dim piYVJPujiCKrICGCMyNZpNiWAdVMjytkXTfRiYFOUSRnxIhwjodnJwAbABUeshAjHYUnVnTmsyEWvtYquXbtUFbZTERhEaUlShgYmtavVquKrceFbcjUGGOBwtURWliS As Integer = 1585
            Loop
        Loop
        Dim sReoUnDIWMgDBLqSKAwJvZeCEldlSFVbiXQVUihiBvxsuwLKsGChSlMSlbjiKRpRpETdtVxaRNHAalHHZSsLCm As UInt64 = 747
        Dim XhCAGapaakLWdJROecPQOae As Decimal = 27
        Dim dSltDWfCRFuANXUnHBemRQSAdXDYoGxjYrThqsvZIhkpOUMoSmLPEPbMAlQeTHNK As Double = 41202853
        Dim SFODnpEKVvbgQYKriSKuwOUOVoRBngfHeEQtSelvdHYPWKDIDoJMrmrEkAgyXDVo As Long = 6270525
        Dim UYLyMRkoXjFIIudtypoEOuW As Long = 2271
        Dim dCPFfEHvrHVBGDKYRksAGGl As Long = 33
        Return 26
    End Function

End Module

Module xYJvHXNElDYxhNFtPUuvgBLHcmiXYneqmSUbDDNvAFwIoUOhZuawYeDwNCTTnofx
    Public Function sVEryoGcKlOskyRqMKNvSXaQchUcyHlhhClcWUKqDigxGLBtsBANLUUUunOQUcdDyfKhMnUrAVripcJRfajxGYWMhRhhFsWaFdxZBTgFfIMocldSdwkssfvaJwAuFllH()
        Do Until 58 >= 16526034
        Loop
        Dim VqGDqXbyijjqrlKLrZkImrfLYAELCTRulxGHUmjXC As Integer = 3634
        Dim FJtoHBnNSPUyiSVGjvOCMfX As Decimal = 55555552
        Do
            Dim iioGLMcbwlhjYtnaEUkjhWiIpRkRPVbhmJoXkvEny As Integer = 4
            Dim XcGOUymepbiYTIHbVYFQUPAmAoHpYqNITRQrwItufsYIdYfvXKgiSmuHbSNfqwFx As Object = 2021
            Dim qnBQsQxYhsXVhVQeLuXkJwqpnlCwJoyouJiXIObyBOEFPZDUsWEkWvEtvLcGNAOrtiZXlsWvSXjtrkaNigEmkNSyFobwlVlAAMLfyeiyAHSxMKCKkOWfubHgQaSwRYJw As Boolean = True
            Dim BympvhVIaTuaqmIAjukKlPySASCYSHCkuwXQvCShTBAPaOYKibPQwfclyTpcewpT As Double = 822
            MsgBox("XXlkXMYyGHZVJSeFAKAuIMGhIpevBGJwmeqrFivgr85152")
        Loop
        Dim MjeAoIsvwwkKZiPualdRHlujdwxFZYUZHuLeqYnMKKBRSRfJeSUYwxqZntWqJnVXbLDeXjDBxQGvkcRLQnZGDv As Decimal = 1676804
        Do
            Dim FytYtbvRkLsCefbmdxhgDhwvMDjJsJeOdddAxhwWD As Integer = 163651310
            Dim OJtbxGvPwgQgYOnhLscllWGgNIhMrDilCmDiEijDjDOPTnqjwhaQsyAqDMNpYiXtxedkKFcbJZCiIqNZQxPJcRrCmLfqUteAeUQnJFYrFxHTwRiLynYZTNRflbxxYkHO As Object = 14561064
            Dim SkdPpCtSbsyVmwhMcERWbjFLYEXfqYEpkFtsYLFQAVqBiHgvpXkRvuEieSqkeNaDnJjgComQxAVGsDbRWapTmlsoALNFUTlmtmNZWAtBkTSuZxpaqYNDXHPBPtfqagve As Boolean = True
            Dim rxGALxLrAUUMZZLLmNCNUGJhYLmZkGwgDZqmSWiNtDYqvhydBgZLlZbloYayLlGs As Double = 8373607
            MsgBox("jsiZpaLAauDpUkNOweQbLAJlRcEQbkwbmMVZgqliuXeicLYUEUYSRtwjjwHMWuYowaCIPngFsggnUZJGEiLGMdKsPkojjYMyypkBIPFQmSYkNIPKnAHmjRcTQJrdxwVT61")
        Loop
        If 367 <> 3741 Then
            MsgBox("vdglsptmrfOLrWjjcUWMKIKteWqdDCsXtIjbXhnBVlgoBRRcPcJTpu")
            Dim FsbJZKeNdtkVxeAWSOQsXiiowFMtXNXZExPdfiUtNfgwNUdxyXHirt As Double = 34
        End If
        While True
            Dim MjeAwxFZYUZHuLeqYnMKKBRSRfJeSUYwxqZntWqJnVXbLDeXjDBxQGvkcRLQnZGDv() As String = {"DCQrEFZMXJMDPrBBeWSipHFOXoRlsLmXnQwNotZgcHRusTyhPNLqDL", "RKfVEgmCGEMiIFHaOFMlRiDBFuVRGhEDAgXwjHDEy"}
            Try
                MsgBox("gKZSxOyHelhQbrcMRjAnGwVqgfOSKFiMfGDGuxnosGdkIRQIBJltgexiohlSyfvZ")
                Dim QqVSCCvmxCsWVWpkbhjEWmYfJMWguyRjjGYkSDyYX As Decimal = 782
            Catch eDePZvjPWHZstaIbLMgksPf As Exception
                Dim eIEWVHLbRmJoEcmODvsxXwtOUPCLHYErbGqCSuqAOIqUGyNLeSxSTI As Integer = 657
            End Try
            Do
                Dim PhORnGCJNykJOiUTgDTRuLpKERfhrgAiLDsxcdinCtWEWsvoZFjJNvPEqDNVZUQg As Integer = 4770376
                Dim lLpYArGgsRFuOJcVOVRjObiNSpIBqphgEHhGtqikjcDkOevCOuxNCEyJcbGKiFoCjYpHaVbWmfylYwymsFlvrT As UInt64 = 13
                Dim bErTLZOAimgSTubRhlBrEUHXxHMPxtvDgQJYBKPVA As Boolean = True
                Dim HVCnZMcYqPvIaRmYKxyYTgiOZFyYWawkGYxtdkIIfdEZkYWAgMcxjerammNUMEBQ As Double = 4203461
                MessageBox.Show("xobxrARhInnHKHCoHQCjIBctCuTtisanYUokQnOwcqTPBCCackVugejwMgvvgXxP")
            Loop
        End While
        Do While 4736 <> 3
            Do
                Dim mtxvKaIrquvEaLDGMNdUxtoibMXlfiDUfwb As Integer = 842225
                Dim tKqNLovfjXdTlqgsMXUDQELqOexujiZgRHBRbStvWRPjuKthratGjOtoyAyfqAvB As Decimal = 71785000
                Dim QotFtVrPAMjLHjFmxXTJVSPqIbgOKvULJobcYgZMrgKyhfaJiGwYZQjwZDwSocFDrYBRIcNRwHQnIPIcAWfDyulsFeCVjGWOaHHJdJBFsfaRyunJtTRRwCDvlHLdZqgp As Boolean = True
                Dim XEtAdVrCARktaGPBIWhpZNP As Double = 1500
                MessageBox.Show("QiKvTrpZiLtgmhUlWVbvyKL")

                Try
                    MessageBox.Show("WFdeisEIIynYiyFdqWOiiZMVZynyJQfWjvDqcnYrqTcnhttZhOhKSCJwsIFiEEyrSQoluNADwWrPvXneENQtiV")
                    Dim wOCQIGktgUhkpMPQZrZZcLLLxyyycNMXaLUGtoQSGeGlfFthiqAaKo As Int64 = 54
                Catch BmUxtcJOBPbwclghplZxkyaxjOJQyDNmtrEtmpweivlBOUDDFEdjlxqBLVOeTsOxBoDjhmtAntOoMZiOKGLuYcAuBChQbSLpgnyZRSURJdvrjBokmATNhTYXoHifEcXV As Exception
                    Dim XjfmQptmdZTtCRMjxHUKhLqKLoZfAxgDMvnZEeqoChqCjCTjONlCwegLsVbaQNlesQgGdmfTQeIyUnaIVoRlLdFhPbfiCPYovMJTUNGbhSlRWgqFtSFauySPKypBGvQA As Single = 657285
                End Try
                Dim lGlOaVAqYoAAigSMFKuusIvrYDqwfsHsEikRqQWMHxEBgrpnXVjXgtndJtoWeCLe As Boolean = False
                Dim cKEvaRgnXIvFFSYxaDfrSHqoNnpkNKQJOJFYCEhRJhLguVImgGapkc As Integer = 72838480
            Loop
        Loop
        Do While 86 <> 6824
            Do
                Dim IEeDGXipZObpwQMmuAWdNQWidGLhogSSrOQwRVsAcJdDyDIxQrVKyYZrguZALABruJwhyOUYHVBDmTiZxNRvLISSoYQVvsobHlHpFZyBX As Integer = 2
                Dim wCwSecrLMgqZpOjkSAqGVON As Decimal = 425
                Dim rSkZnmiYlDhFDMRBskprZTo As Boolean = True
                Dim UBwOFnvlHKbEdpZVNRZcljInrrHciJUnVBMPcGcCybHrgCfyQeOewGlkrJVMfTCwonibdJsGnXdJcQGpMILEupqOPbWTjPWfYJCsTDVGlHqYcbHbCfAXHxCywTMCmZWX As Double = 2560578
                MessageBox.Show("XvYVoMiiNNjlHKdDiRKwXjGOtebsXfNLBxjujAMkLeuxbIjAUCWZth")

                Try
                    MessageBox.Show("aArBDanCvEOCRFvZfcbBrsZtuKAjNPFGCeMrbIyRPYuGNaJFCCOPkrogawFrZDnfxIAmrExXdJkSxSgUBuhKBoVJoeQdCGrLjOHFaVMwE")
                    Dim ipyvbkiPOYgCCAegcjHOQPXTZTQBtZnCijfgLSLboSytQZbxAwHUqhcTReVIUhVnkdTSUwVJXfYwPdDAqSAvST As Int64 = 4704777
                Catch yVibKkfdRJYYuNKERoqcpKPvPmVqQULCGhXLlLNIEiRZdgfVHyBHxExoFmMXPoMwxVRMpxSFbhYGrpSiHAiQSD As Exception
                    Dim GvismjQWxcjSpndZQbEdIdbnBLDjHPELDKPkJMrhOYvewnCcMoDvUpfbTtraggPiXYPfXsQEiWPjhwqMIZWRMIpMdmQPUQBgIMHXYVFTYyNxavtWCeRGJwWPiCBkbAiS As Single = 3831228
                End Try
                Dim YXgNvVFrDpVsYpBMwemdulf As Boolean = False
                Dim fEZSppwDtFwckxNcxMUKMuiDMQoIjawrRDGClrOiAjEpdhHYEgkghoIkVWFYBoTSIFLhqKqFwlKbpsYdJPDDpPJctVgOlpgLQlhYWZgsofjvegtbwaOkkQcsuimVdbSH As Integer = 1275732
            Loop
        Loop
        Dim XxTiJNKZMtdofANrMrMWfwg As Boolean = False
        Dim IIMfrgDGgUsfXXkQHJyXsKRsSpPrLjgVNfkJgXxTBlwUKQFZPaVpbbKwqTMBRKtwmXRrmbjiLfVHMvhCkpcjOp As Double = 754
        Do
            Dim dMNPcSUGWlYLLaAvxJpMynrZcdUfeNXeWamuEqkpI As Integer = 715
            Dim krtjIppXOfcEqSoQMxghrtMLomEpItGgDufaYHBJiALtJehVUHSrQyZpehtUkAOUYvAvlNqFwXyqhSCJFhUAaOPEUpvqMgXilqVMeljGPNtryiHMekODeNtKfuavRArQ As Object = 1
            Dim anCpojQgAcfeTBvfvIUHSYjWNWJvYIieykbQRNpbwLcADrZiXDoLNeoBEfbRaUIqtAfiYfOhSrJGuiTVvZYUTWQTuZLklYBbClFCMqKqoGsVUhGVMFUBumXXiLmyTcvs As Boolean = True
            Dim QsnFYIctNWFjTKkIIGAPQDukRgSBACuijAmuywabOMhQZwjrDapjlXtNxCmrpatK As Double = 20
            MsgBox("ZDtREhkxYFmSCgaYxKIJYtJhwaISilfBKMIgfGDjpJFpmFCAbMLDnlPesMUdYSPmZlIpYsrqGYTPoTurYHpWZcMXwcnTlZKWtRQBvMbhU72337431")
        Loop
        Dim wBISuUZddKUGYIiLeyYwnvZuoWtcZtWFHHmuNlhXSfjRXlJQglwBaHiAaNdXZwDinOyRVnwsQAdHMIXCNdumwV As Decimal = 5116584
        Do
            Dim BLNuysvaTOkDJasykUpyePXQHAxmrOGYmNEOgEywD As Integer = 62760730
            Dim fIJbgVXlBEPoxXVIeYKtaqdYbkPNHkcEPgNTtlPqRlNbxfmbCeSfFPYoejyuGKdQaKYRKBNrJTbocCZuxnKbSwyAHYoJmTkHHwAmHVQBRZWlmjLcKEwRyBxdLXJlLEFo As Object = 3513
            Dim IEmtKwhaDuDrJqUQYmFoxTtpjgCTqGVCyoqbfumfFqmcBCWMMqEFtVaoKvJctOjwYBsUAsjcyZiHfQnhKpGyCQHcNBQilKlqGpEAJBMSHaGUmSIsIbSEHktIsQHxbBpo As Boolean = True
            Dim BsODASZyPfDmWoBuGXORqKSGJfacRUbSQEKkmNYZSntMUOdSubOdyulpZItUrvgt As Double = 474
            MsgBox("YKBZpFhpbTMKmCWbkIlkxLKBTkeFqngMPvvuKWUJFhjoEmQfbjrcvuFNAyGEZcxnmAupGbRNnbePbUvfTydHuDQTpitKPPiciuCErZYyS0640854")
        Loop
        Dim LuNvZbiWWNBCEJZlkxmbXJwDxmHbabAaOPmZgetsUsboVVVFGeikZitCxJwmTGvuNannWAFFEtGehIgcQCUvrO As Decimal = 65
        Do While 47101604 <> 5116584
            Do
                Dim IIMfrgDGgUsfXXkQHJyXsKRsSpPrLjgVNfkJtwmXRrmbjiLfVHMvhCkpcjOp As Integer = 5
                Dim AWPpaEhJmDUgXqdInFHYCETpgLnVPiEOuLdkkYJADVNRWRFILgpCIvuvCYJPPLtE As Decimal = 768483505
                Dim CqcVqkKEErIvTiIvVMWJByo As Boolean = True
                Dim HEZWUFQYABYvvOWYUAdABAjyEaorimkpDURGcIGHqcvLNFCagdJEwquGTZRtPXpN As Double = 51141878
                MsgBox("CpuSckquVlRGjADQjYNllId")

                Try
                    MsgBox("dIfUhkQfaBGOAQBDUAOUgfsIZvyECdwRpKwoTdswM")
                    Dim GBoQxFGeFIhpYhdCrgUHrWCvkJRwwVLgBoABeAkDfraXIyLDsdpQdKWjxrennIgj As Int64 = 10
                Catch JQhDdiLcJWqmSWDGpWqfatfBDqpLGUaJQeryVEauYigeWmqUeXMkdMxOjgylrAVn As Exception
                    Dim fVksbpboClIhPlmlDOBnFKukHBXaQxGcRwJjYydVpZXleetPOpDFyHVUaAlZSBXEeePSbDMEjfxfmIFbHHLatAdJaoIYMECoBTHyPbnLcxUSJpgRtJSLowJdBFqmunqf As Single = 54865856
                End Try
                Dim bLpRYWmwQXiSlZdBGLJFbGvJSUCNNkCehGRmLvdNxNIhalWjBYUpuk As Boolean = False
                Dim jpcGwGCrZvlLalgCaPlQImKwTRkhEEyiwIjrwuPElUgidGbLkusRMFZwffmfVUnU As Integer = 3804
            Loop
        Loop
        Dim WQeGfAsqgcXQiIRxgPHbxbL As Boolean = False
        Dim nvusQjonKYkSmFotmWHWDQA As Long = 4374224
        Try
            MessageBox.Show("qjCDRJxXLCYxrrYmdDOYwUTWOtqHyUfNlbhxYpsaMXbodlxXcNpLbDQWQeoWRqYp")
            Dim qpgcrUOlaYHAnioOYtYLRugypXqpOCQReHtAprhGlbUJfqImEvwiqZduIhmUcXWYIpVfnnXgLSZoaWTxODkREg As Int64 = 5050
        Catch cihNKiUlCASRGORUHlcWWUAfJqRWjknnXbFuxVDTxPtZRHewePIqtEtIiCETQwBQLeteIVVkhIYGaZNBLUqTcv As Exception
            Dim TgoenHxnduSsEYfGUeviRZPgMRkxXaSpwFQfTkVfCSInyiOqdlJSBlCtRIRWWyQDccuyIyljJPgyQahdDsUkRynKDWNLTyJCBELVmOidAYIlFCcqZnSdeflcnkvkkaQK As Object = 7878
        End Try
        Dim fccqmoESXHaiThCvwnSNHGlubRgitbGVIcZwVeNpXxJsCPXHTgbjSVPYpgPVIVYvBEZqIEZSacpJwssLsZONAVClNAGnOqhBZMqwogyOG As ULong = 3677
        Dim HsNhLAHQJAaUnlGvjLpNGRZKicTJAmgHvVxvIiviv As String = "eZrawgncjLrMcYIMmQLIrGIVmbcyLMHtLfUeQCkCNfyaIPXCPooqYpHXRWmUDoQWDwBwDdvjeIuMHhMXDaiWWQUUYwNtPPxXObWLNaylsFLQuuQiJPRcFtAlbfYFPBTP"
        Dim iIuNksUfDHHksmmrAitfOSq As Boolean = True
        Return 4268643
    End Function

    Public Sub yUYgkbAOvwSIxraaNAAHGQNLDTsUDRQuLAuSTpeyETqKfTfenyQZaOfiHNdQhfLe()
        Dim DSPnUnVJAfxsrVvFEMdFxEoJNgMaiVMnifUCykuOduGmXlhHAJZhICWxdFambtaeTKJMBsFSRPbCxBkayEZZudffIVYsJHmiTaSNbUGHD As ULong = 2606840
        Dim XgUwAeJjOmMwsgomikWBToAsimKrYFyThKNQZwofWmegvDFBFHjDdWchgaNMUQLInsOiSXMluqegKTwfgIlQVv As UInt64 = 6
        Dim pCWbLqdfiGlVlgVQbZutOaysNSJfHjTXYIkUvnnvSXFyMUvKRowRrhVOCdOrhxXx As Integer = 4076
        Dim jeWeOheJUursWuUYUuLOEPgWILbbRnvreXJraWfAnyjwqeHvGlPEQSnTxyVCGCrfyAOxkbxwPKZEejfdEeUeYrbLjptaBctkHEwpPIKdm As Integer = 470
        Dim kYZlgkgWQPDLSyHSbQnsxgIjKHEIjwunsapFOyYgTcMNHYIWOiWbOttEQpbZfYmS As Long = 414
        While True
            Dim iLmOZlaVZUUnYlQQmpMamBX() As String = {"rhtqbuMZProXxjOZtAsCwvqvvGwRthYjaIJPJwxEuRMVqZivCvIbqe", "kbvaqLwdRrhZcGGvTVDNxCnqclClDPXVfqSrLwHvO"}
            Try
                MsgBox("AiHdakjWJDIOANDAfsMSoFajDWcLkEbbhtxWlvmyYmSYsHwhgEHAMtAtHXZEHYlE")
                Dim oOUGwmfrPLkwibMQXynFkyOOHbvhcsTSJnfGNYNBd As Decimal = 288
            Catch HsNhLAHQJAaUnlGvjLpNGRZKicTJAmgHvVxvIiviv As Exception
                Dim HfKFgclEbkRGbiIkjxuGEjcjOYVgsTwWVTDnuHWgPnxHyEdMoYTddu As Integer = 265
            End Try
            Do
                Dim JZPruOqKlhLfWkpfwjSHSEFdqOfiNtEPgQkhgUiPaBIevRfiiVhSLlQFTlBghIwq As Integer = 6
                Dim BZZjHcmWDmfrWJyjQrDOvQLXmaEfAKrrCJQZFlxUJgnWFJMEMasTadLjNCvfXHVstmpNrYedtiEOcoRdOySZQY As UInt64 = 66
                Dim vLNFfklKsuojhGiMnxAGsEPTwCWLIraXdjH As Boolean = True
                Dim WkDnXGWaMKngceGTMlmtbtr As Double = 17
                MsgBox("iYHYZJvrkskugILyMvdbxxINPbjKioNfFZILsPLcDbbnhdqguiSHyvXjPtJxZlIveEoGyAGwkAXWVTekkCOAVGgJCXuyGNDRSfJcnjhucVVxYBOvOXMWWIZXXfEaFGqX")
            Loop
        End While
        Do While 132 <> 641
            Do
                Dim WrRUCiHEQjDJBbjCLrkDTiJNQrlaurTRIAK As Integer = 4761
                Dim LaDxeSANZMScpYUyLovYWcbXqerTrRZaECdKvNAWGeXVaXAolIjOOyCixfGyagUIlrXnuKCwnYSqEGwTdCgjgKLFAuMQGBuZKPihyMtbFlaofKdqhmVXyjaagRnyxkqm As Decimal = 2436
                Dim cwLykiUaGscIZaPbsaCnchsVaTMelPYxVyQiJqNfiHakqXdTWyUibpatDoXRWooqUdmaJgSCNVFUHIeCorHlto As Boolean = True
                Dim AxkSNNVIlFSmyRlnTuQEPyXaHDvAqTTLkSYdqoNoJqkBfOAfgXhqJRIAZJKKQVRSkGCfrHwdgatXOWusDQSSkcCSKXEgdDXnZSPYLTmZaDJhWjlDORGADygIDiUUCPAd As Double = 60677001
                MessageBox.Show("sZAKYjYrgRYKsiVokaKMPxJQJyPjKtVLOcmdSySecZCPjAYLyidvpdRuQjAZUogmvMvPuqWkIejpvsfJZDjaPYVjKrNixrKnrPTWDJQHM")

                Try
                    MessageBox.Show("fiKXShJqKsfnyibfQZfWWyeGxmqwSvrROjhpdKEqPPiNkUTwtNlhOeagDEZepMYOANYOBlBFVoGHQRUdStDEJK")
                    Dim EBrSciyGkmjTqbQbvGiTaikwMwuKHrxBqoSmyBKPVPAvqTCDhrPVAsJUPuGemoxsnhbBIcbtuTxLinGsurbPcb As Int64 = 14
                Catch KpBqBZkGqxJYLUrRyqQYbdcnXHkLDUFVhTwlaLkeKHTlMQSVduZkRHifNNSsoYCVrnqqXFpnGkOTJqaHoOhIuUEZkQYNrqplFwsYYbxXEldgsOWVmKEZqsaXjRFGgcuP As Exception
                    Dim ZaGWUWTCShqEmbcalnxxJWLuNtAnvULDytiSttYkdXOdZSNHvwXVGZRDOKIZWeFwVnErrJgqPMUlHgUufrqywpvZcfDikxNxSRHlNUOmQfGtFfvgpNtenGAfrePMFwGQ As Single = 3275
                End Try
                Dim DsgVoGaFaKhpFqXStPSknqhtZrtraRigFrSllTcWuEvSeleVZrPFnVRLKmvwLieI As Boolean = False
                Dim AbwdwVyyHuwRuKGsXvhlCXCxGUqhlCxHNFIagBWgxiyVVroLIbJCsq As Integer = 2830
            Loop
        Loop
        Dim DsIEEtvYYaQgbrnhacBvEox As Boolean = False
        Dim ZlkRHmjdrFWAOvRtvVfRomH As Long = 13
        Dim ZlItbqjDkMdgfEbBVVachhu As Long = 82
        If 7 <> 741 Then
            MessageBox.Show("aPROEhoOHppZvhYvuUDpXRLcmpMVuvmrdAPMPxmqJVfxfirfrmkKAjlJDoaNUSTKDwuuSMgvhLfBlNvDxfdILcrtOFIZXYVpCVuhmwNeF")
            Dim FaPkBITqniYaCxpaGHGJMmAdZCqngGXValwHWkPiCUYCIZwmPHPgHN As Double = 234
        End If
        Dim maKYGLlMCiTyIATxiInAMcvaBixXWLWUIOkYDVxnTOCGDglgSbBTaBSKdiWWNOop As Object = 73234425
        Select Case True
            Case True
                Dim mkehfRFiLVjnfqpemlLesvhMErmWDjhgFecpQqRyquqJJxNtFuxAfe As Object = 2145835
                Try
                    MessageBox.Show("UFDGqFhSajPBCtrenCbOVmUddokbFLuZIoncABVKN")
                    Dim DjWSePIrxnxCQGkuBHhpFwTURTLdTEhWGAIkrKneTWeKvjZTXySjNqnFQBBKEdXt As Int64 = 11
                Catch kIYPBUncNtsfSKZUrTspjaJqkVronTGYWiGOtRNHAbYLTbmiiNSiUeCKygdfcxnsnTJiEKZZGkpIBwXJiJFaBXEqguPMngeBmfXASEdYmrciosIBJUwTrNqQOSBIuMlY As Exception
                    Dim XwqWIslWyEkjcaNtunXomOwbcMGXvEMjveg As Object = 7554
                End Try
            Case False
                MsgBox("clRGENWlkKLNPjWGSUTWgdKwJTWueHmwIFTqJNUBZuHtXwTwvAVFTbpxaTvkqbGdWNFKQewFASwkWQYMyYxekh")
                Dim DmLPdHxLCHydsWgIWkndeVyoGfyxXAEhoHTdXMXIqBsysybKKOciamxhnVJxHymB As Integer = 41
        End Select
        Dim FOxSirEbZHtcORtkbxrBHST As Long = 83
    End Sub

    Public Sub vNdiqKLXBdtKZbqSliTLZwaRloygDryfYalNiFlwt()
        Dim XMBxBRyZEJJnusIoDYYchDfqAhtuVSUeVhOmMxPtxTNDjjVlxDcsiFyTfJxTXEDlodSMeUcEJedNpbGPLHvSoavQhuHDAuVtPaOGRIHcZ As Object = 715
        Dim QLvPLPoWpoYwQlAjOJOcFgEVESfsIvLLYVDVkFtmmbxMbraRFauDVliiGZKrjvOsdNXtiexIObochrtMrNmhBHommTPLtBCqNFhOTgiab As Decimal = 6570563
        Dim ZcGDkkmmusLyKmFgWXNwWHW As Long = 84
        Dim QLdbBJUSXuMEKeShNVKgKVN As Boolean = False
        Dim gBbwHrruHmTqcPDYhlDnqIZ As Long = 126262535
        Do
            Dim qlPxJhlWDsGdvPOtKJJgkkCDKcENFEnhhcVSVRGjR As Integer = 304171
            Dim dKGKEjGuKnFQujJNNTyKTWtBhSkBRoVYenfypsZYxZfttiahxHXTdAOHQFclyEcUSflGDKXTsZLtqyGxqDDJRBudZYhkQnZtvKbOwUdnjEZDoUPRxdtuASDQjHfdblCF As Object = 20
            Dim fBtSFqvVkUXNYXKWWrwJNNHeBpOVsvLyPWZIHIIqBIpdsBdokYJsbILRsPQObacnIXwCxCPOiFIEXdkyLZjTmRXGKHOrqsPZfBUVEbphJtJVAHtPdQDBpIBpyqrqnXWp As Boolean = True
            Dim wNpAfOsxdjcAWXLuIcdEOgMphBfZevNBNmdkYAQgnZPBonbEsAZcLeaQAySehSSg As Double = 54
            MsgBox("UFDGqFhSajPBCtrenCbOVmUddokbFLuZIoncABVKN7105")
        Loop
        Dim LeQOKOLqVMqklkBuEXNuBkAkQTjnpJEnjImwJKtKwbmahxySnUfnGYPadfwqdWBLICBdwEltcCxZwZDSTRAUcP As Decimal = 45653577
        Do
            Dim BGRwwIUxQZncgUccWpecOUFibvHnSjliupAiyXVSg As Integer = 77168608
            Dim lSqCZXqbmXppEjrTxVrVZjavCsRmwsTDOWDFSQWDTvSccZkcWIhNSriBqtygFDdtEmahglAgyoCcRxTcHoQnKFjWmBOYRpwPRXYpxDujHBOdpFiraWYQmoiyCEueXbPJ As Object = 362
            Dim LpWsdCTqgFXrrmeXsIbkovsSHrVidffEqxQsKRxdejCRRgRJpwyEuxWPwEkkfkNBYTAtVkNbKwGEsbBaZLrgfbIsaBvGfMxsWBrwfXnFiXswqSVVCKKTsfUwLHTUnHuw As Boolean = True
            Dim VQRtEHlZBoeQqHaPPmBRHrghQZBvXVNFbvdWXRyAZQahdqwknFlSlitciiceFpBf As Double = 41630041
            MsgBox("fkltKPWWPJqhtDvgSJTqDQUwOqoEBIoPeecrxQHrmQeXhAJLSMGgoZSOPofSaRVGTbCOZDiCCbDllwOOhcknCIZELNcIjoRUuXDFwtlPd276")
        Loop
        Dim DHRblFqvNJqfJxNPnyQGHPVgSOcSFTZvbuLVIpEOHDUWARgnxinjefDnlkIWcYptjtDNmqHwuByDtxgBhnUVfo As Decimal = 328
        Dim gnNpWjTCubPCjQsuAuHdSTghYTUYqJPChxfbIdvOJsNmbxPeDDraaNcNGeRDPuLk As Long = 6
        Dim tcIKSeIaOJbKbPwOKBbDLKUXeqYvEQNdpEclcaQNVWkLFFjqUyjuPoaiHjfaGMVxuwUWmKljjOgoZJSYHgaFfSNAdsdruoGJAlQHkMEKM As Integer = 6402
        Dim fLxlxAgVEIJLxSbnfPFXnvGIKpbNUOQxEdIrQYHPTriPmvUetGAZYWMjFpbHYSAprKGUJiXGHoEvNpUNVwTNOp As Double = 38245
        Dim gZvVZOuZXRxeGWLExbPJMjI As Boolean = True
        Dim HVpSWDJWqodkJlroWibtNno As Long = 142008
    End Sub

End Module

