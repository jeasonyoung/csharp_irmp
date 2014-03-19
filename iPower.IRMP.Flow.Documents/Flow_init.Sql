--*******************************************************************************************************************************************************--
--	流程引擎 数据库脚本 枚举 初始化数据
--*******************************************************************************************************************************************************--
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--变量定义
declare @EnumName nvarchar(256)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程状态
set @EnumName='iPower.IRMP.Flow.EnumProcessStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Stop','禁用',0,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Start','启用',1,1)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程实例状态
set @EnumName='iPower.IRMP.Flow.EnumInstanceProcessStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Stop','暂停',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Run','运行',0x01,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Complete','完成',0x02,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Error','错误',0x03,3)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程实例步骤运行状态。
set @EnumName='iPower.IRMP.Flow.EnumInstanceStepStatus'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Committed','提交',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Suspended','挂起',0x01,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Complete','完成',0x02,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Error','错误',0x03,3)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--步骤类型
set @EnumName='iPower.IRMP.Flow.EnumStepType'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'First','开始步骤',0x01,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Middle','中间步骤',0x02,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Last','终结步骤',0x04,2)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--步骤模式，分支汇聚。
set @EnumName='iPower.IRMP.Flow.EnumStepMode'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'SplitAND','与分支',0x01,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'SplitOR','或分支',0x02,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'JoinAND','与汇聚',0x04,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'JoinOR','或汇聚',0x08,3)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--步骤通知消息类型
set @EnumName='iPower.IRMP.Flow.EnumStepWarning'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'None','未定义',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'SMS','短信',0x01,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Email','邮件',0x02,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'IAMS','消息',0x04,3)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--流程中各种操作参数的类型定义
set @EnumName='iPower.IRMP.Flow.EnumParameterType'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'String','字符串类型',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Int32','整型',0x01,1)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--变迁规则
set @EnumName='iPower.IRMP.Flow.EnumTransitionRule'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'AND','与',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'OR','或',0x01,1)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--比较操作符类别
set @EnumName='iPower.IRMP.Flow.EnumCompareSign'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'None','不可比较',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'GT','大于',0x01,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'EQ','等于',0x02,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'GTEQ','大于等于',0x03,3)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'LT','小于',0x04,4)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'NEQ','不等于',0x05,5)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'LTEQ','小于等于',0x06,6)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--参数映射模式
set @EnumName='iPower.IRMP.Flow.EnumMapMode'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Value','直接传值模式',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Reflection','函数反射模式',0x01,1)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--数据类型枚举
set @EnumName='iPower.IRMP.Flow.EnumDataCategory'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'AddData','附加数据',0x00,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'FlowData','履历数据',0x01,0)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--任务进入模式
set @EnumName='iPower.IRMP.Flow.EnumTaskBeginMode'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'None','未进入',0,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Normal','正常进入',1,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Authorize','授权进入',2,2)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--任务离开模式
set @EnumName='iPower.IRMP.Flow.EnumTaskEndMode'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'None','未处理',0,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Normal','正常离开',1,1)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'TimeOut','超时处理',2,2)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'TaskSeizure','任务强占',3,3)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--任务类别类别
set @EnumName='iPower.IRMP.Flow.EnumTaskCategory'
delete from tblCommonEnums where EnumName = @EnumName
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'Pending','待办',1,0)
insert into tblCommonEnums(EnumName,Member,MemberName,IntValue,OrderNo) values(@EnumName,'BeRead','待阅',2,0)
-----------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------------------

