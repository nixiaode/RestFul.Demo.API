# RestFull.Demo.API
asp.net core restfull api <br/>
前后端分离，纯后端接口项目示例 <br/>
基于asp.net core 2.2，按照restfull标准实现的纯api接口项目。主要用于公司内部开发人员参考使用。
# 项目：RestFull.Demo.Tools
工具类项目，包含自定义异常类，自定义返回类，入参标准类实体。
# 项目：RestFull.Demo.DataModel
模拟数据模型项目，此处使用MemoryCache存储数据。
# 项目：RestFull.Demo.Services
业务实现类项目，实现数据增删改查等相关业务。同时，也包含了业务入参、出参实体对象。
# 项目：RestFullDemoAPI
asp.net core web项目，包含自定义异常过滤器、自定义方法过滤器、多语言实现以及Swagger组件。<br/>
本项目为启动项目，默认不启动浏览器。访问swagger请浏览http://localhost:5000/swagger
