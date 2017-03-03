;#DropDB TypicalDXeXpressAppProject_DoSoEasyTest

#Application TypicalDXeXpressAppProject_DoSo.Win
#Application TypicalDXeXpressAppProject_DoSo.Web
#IfDef TypicalDXeXpressAppProject_DoSo.Win

*Action About...
 
*Action OK

#EndIf

#IfDef TypicalDXeXpressAppProject_DoSo.Web
!Action Navigation(My Details)
#EndIf
