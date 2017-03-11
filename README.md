帮助文件请点击 http://www.cnblogs.com/chenxu520/p/4655738.html?utm_source=tuicool&utm_medium=referral

UIAutomation是微软从Windows Vista开始推出的一套全新UI自动化测试技术， 简称UIA。
在最新的Windows SDK中，UIA和MSAA等其它支持UI自动化技术的组件放在一起发布，叫做Windows Automation API。
UIA定义了全新的、针对UI自动化的接口和模式。 分别是支持对UI元素进行遍历和条件化查询的TreeWalker/FindAll。
定义了读写UI元素属性的UIA Property， 包括Name、 ID、Type、ClassName、Location、 Visibility等等。
定义了UI元素行为的UIA Pattern， 比如Select、Expand、Resize、 Check、Value等等。 
还引入了UIA Event接口，可以让测试程序在某些事件发生后得到通知，比如新窗口打开事件等。
MSDN里的介绍确实非常详细，UI Automation的MSDN文档在哪。在这：http://msdn.microsoft.com/en-us/library/ms753107.aspx　
我们只看关键的一节： Using UI Automation for Automated Testing 上面的文档能够在你遇到各种复杂情况下有资料可查

但是对于一个刚刚接触的人来说，大而全的文档反而使得无从下手。 为此我在UI Automation的基础上根据平时遇到的情况，对各种操作进行了封装，
目前该版本1.0.0.0还比较粗糙，在使用中或许会遇到bug等情况，请大家指出，并一同完善，谢谢！
