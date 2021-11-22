# LC_MyDelegateEvent
委托异步多线程：.NET会将所有的异步请求队列加入线程池(异步请求队列)，以线程池内的线程处理所有的异步请求。每次异步调用都会使用其中某个线程执行，但我们并不能控制具体使用哪一个线程，具体调用策略由操作系统决定。异步方法其实是执行在线程池中的某个具体线程对象中。

1：委托的声明、实例化和调用；

2：委托的意义：解耦；

3：泛型委托--Func Action；从Framework1.0-4.7 Core到处都是委托，Func Action 异步多线程 事件；4：委托的意义：异步多线程、多播委托、事件观察者模式

    /// <summary>
    /// 1 委托的声明、实例化和调用
    /// 2 委托的意义：代码重用和逻辑解耦
    /// 3 泛型委托--Func Action
    /// 
    /// 委托无处不在
    /// Func Action 异步多线程 事件
    /// Framework1.0---4.7  Core到处都是委托
    /// 
    /// 4 委托的意义：异步多线程
    /// 5 委托的意义：多播委托
    /// 6 事件 观察者模式
    /// 
    /// 委托就是一个类 为什么要用委托？
    /// 这个类的实例可以放入一个方法，实例Invoke时，就调用方法，说起来还是在执行方法，为啥要做成三个步骤呢？唯一的差别，就是把方法当做一个参数放入了一个对象/变量。
    /// 自上往下--逻辑解耦，方便维护升级
    /// 自下往上--代码重用，去掉重复代码
    /// Action/Func框架预定义的，新的API一律基于这两个委托类封装。
    /// ***因为.net是向前兼容的，以前框架版本定义的委托也是兼容支持保留着的，这是历史包袱，此后，大家就不要在定义新的委托了***
    /// 
    /// --------------------------------------------------------------------------------
    /// 
    /// 事件Event：一个委托的实例，带一个event关键字
    /// ***event限制权限，只允许在事件声明类里面去invoke和赋值，不允许外面，甚至子类***
    /// ***事件和委托的区别与联系？委托是一种类型，事件是委托类型的一个实例，加上了event的权限控制，仅此而已*** 也可以理解为事件是一种特殊的委托。
    ///
    /// 事件event真的是无处不在的
    /// winform无处不在---WPF--webform服务端控件/请求级事件等...
    /// 为啥要用事件？事件究竟能干嘛？
    /// 事件（观察者模式）能把固定动作和可变动作分开，完成固定动作，把可变动作分离出去，由外部控制(扩展)
    /// 搭建框架时，恰好就需要这个特点，可以通过事件去分离可变动作，支持扩展。(事件就是一个开放的扩展接口)
    /// event为什么要限制权限？避免外部乱来。事件不可以在外部直接调用，只能通过内部声明公开方法给外部调用触发事件或通过修改类属性时在类的内部调用事件。和委托不一样。
    /// </summary>
![image](https://user-images.githubusercontent.com/26539681/113878681-69522800-97ec-11eb-80ad-c8aa29ef0791.png)
![image](https://user-images.githubusercontent.com/26539681/113878754-7bcc6180-97ec-11eb-8015-a9e4a0de08ff.png)
![image](https://user-images.githubusercontent.com/26539681/113878806-85ee6000-97ec-11eb-8730-52281ea90384.png)

希望为.net开源社区尽绵薄之力，探lu者###一直在探索前进的路上###（QQ:529987528）
