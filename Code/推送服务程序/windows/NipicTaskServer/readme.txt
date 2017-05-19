COM 组件 C#
$/Nipic.root/NipicCOM
需要注意的
// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(true)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("3a643c95-b368-43a1-acce-b1d189642a16")]


2.需要注意.Net版本
32位 64位

3.方法不能使用static修饰符否则，不能交付

<%
  Server.ScriptTimeOut = 500000000
  
  Dim o, r
  
  Set o = Server.CreateObject("ZipUtility.Zip")
  
  response.Write(now())
  
  response.Write("<br>")
  
  r = o.ZipFile("G:\test.eps", "G:\test.zip", "", "G:\QQ发送.txt", "")
  
  response.Write(r + "<br>")
  
  response.Write(now())
  
  Set o = Nothing
%>

<br />
===========================================================================================
<br />
使用以下语句注册Dll，注意注册程序RegAsm.exe地址，64位和32位有所不同。注册文件地址Zip.dll
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe D:\昵图\COM打包\ISpringJob.dll /tlb D:\昵图\COM打包\ISpringJob.tlb /codebase
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe D:\昵图\COM打包\VideoJob.dll /tlb D:\昵图\COM打包\VideoJob.tlb /codebase
C:\Windows\Microsoft.NET\Framework64\v2.0.50727\RegAsm.exe D:\Projects\Zip\bin\Debug\Zip.dll /tlb D:\Projects\WebSite\Zip.tlb /codebase
<br />

取消注册
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe D:\昵图\COM打包\ISpringJob.dll /tlb D:/昵图/COM打包/ISpringJob.tlb /unregister
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe D:\昵图\COM打包\VideoJob.dll /tlb D:/昵图/COM打包/VideoJob.tlb /unregister

C:\Windows\Microsoft.NET\Framework64\v2.0.50727\RegAsm.exe D:\Projects\Zip\bin\Debug\Zip.dll /tlb D:\Projects\WebSite\Zip.tlb /unregister
<br />
<br />
调用接口函数<br />

<!--
/// <summary>
/// 压缩文件或文件夹
/// </summary>
/// <param name="arrpath">需压缩的文件或文件夹，多个文件可以用","隔开（包含路径）</param>
/// <param name="zippath">输出文件地址（包含路径）</param>
/// <param name="comment">注释文本</param>
/// <param name="commentfile">注释文件，从文件中读取文本作为注释</param>
/// <param name="pwd">压缩包密码</param>-->
ZipFile(string arrpath, string zippath, string comment, string commentfile, string pwd)
<br />
测试文件：1.49G<br />
1、压缩后1.24G，使用时间3.5分钟左右，脚本超时，不过压缩依然成功<br />
2、使用RAR软件直接压缩，压缩后1.20G，使用时间近10分钟<br />
结论：使用ZIP算法速度快，但压缩比不高<br />


生成ppt预览图的方法
Set o = Server.CreateObject("NipicTask.PPTConverter")

// 设置输出文件路径，外部程序调用时会用到
public static void SetFilePath(string filePath)

<add key="FilePath" value="D:/昵图/网站程序/load1" />

// 设置数据库连接，外部程序调用时会用到
public static void SetConnection(string connection)

<add key="NipicFile" value="Data Source=192.168.1.167;Initial Catalog=NipicFile;User Id = sa ; Password = huitu168!;max pool size=10;min pool size=5" />

// 处理PPT转换
public static void GetThumbnailById(string id)
GetThumbnailById(string id)


// 视频文件转换
mov avi dvd mpg flv mp4 rm rmvb
// 需要创建的类
NipicTask.VideoConverter
// 设置输出文件路径
SetFilePath()
// 设置数据库连接
SetConnection()
// 进行转换，生成预览图
GetThumbnailById(string id)



reg.bat

RegAsm.exe ISpringJob.dll /tlb ISpringJob.tlb /codebase
RegAsm.exe VideoJob.dll /tlb VideoJob.tlb /codebase

unreg.bat

RegAsm.exe ISpringJob.dll /tlb ISpringJob.tlb  /unregister
RegAsm.exe VideoJob.dll /tlb VideoJob.tlb  /unregister


C:\Windows\SysWOW64\cscript.exe  D:\昵图\COM打包\test.vbs

test.vbs

WScript.Echo("测试32 64位")

'Set o = CreateObject("Scripting.FileSystemObject")
'WScript.Echo(o)
'Set o = CreateObject("NipicTask.VideoConverter")
'r =o.GetThumbnail("D:\昵图\COM打包\mma.avi","D:\昵图\COM打包\Nipic_43_20130327110151477101.jpg")
'WScript.Echo(r)
Set o = CreateObject("NipicTask.PPTConverter")
WScript.Echo(o)
r = o.GetThumbnail("D:\昵图\网站程序\load1\file\20130402\44.ppt","D:\昵图\网站程序\load1\file\20130402\Nipic_43_20130402084130242101.jpg")
WScript.Echo(r)


