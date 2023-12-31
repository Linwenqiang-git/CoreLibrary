﻿using System.ComponentModel;

namespace CoreLibrary.Core
{
    public enum ApiResultCodeEnum
    {
        #region 1xx消息 这一类型的状态码，代表请求已被接受，需要继续处理。这类响应是临时响应，只包含状态行和某些可选的响应头信息，并以空行结束。由于HTTP/1.0协议中没有定义任何1xx状态码，所以除非在某些试验条件下，服务器禁止向此类客户端发送1xx响应。[3] 这些状态码代表的响应都是信息性的，标示客户应该等待服务器采取进一步行动。
        /// <summary>
        /// 服务器已经接收到请求头，并且客户端应继续发送请求主体（在需要发送身体的请求的情况下：例如，POST请求），或者如果请求已经完成，忽略这个响应。服务器必须在请求完成后向客户端发送一个最终响应。
        /// 要使服务器检查请求的头部，客户端必须在其初始请求中发送Expect: 100-continue作为头部，并在发送正文之前接收100 Continue状态代码。响应代码417期望失败表示请求不应继续。
        /// </summary>
        [Description("服务器已经接收到请求头，并且客户端应继续发送请求主体，或者如果请求已经完成，忽略这个响应。服务器必须在请求完成后向客户端发送一个最终响应。")]
        Continue = 100,
        /// <summary>
        /// 服务器已经理解了客户端的请求，并将通过Upgrade消息头通知客户端采用不同的协议来完成这个请求。在发送完这个响应最后的空行后，服务器将会切换到在Upgrade消息头中定义的那些协议。
        /// 只有在切换新的协议更有好处的时候才应该采取类似措施。例如，切换到新的HTTP版本（如HTTP/2）比旧版本更有优势，或者切换到一个实时且同步的协议（如WebSocket）以传送利用此类特性的资源。
        /// </summary>
        [Description("服务器已经理解了客户端的请求，并将通过Upgrade消息头通知客户端采用不同的协议来完成这个请求。在发送完这个响应最后的空行后，服务器将会切换到在Upgrade消息头中定义的那些协议。")]
        SwitchingProtocols = 101,
        /// <summary>
        /// WebDAV请求可能包含许多涉及文件操作的子请求，需要很长时间才能完成请求。该代码表示服务器已经收到并正在处理请求，但无响应可用。[5]这样可以防止客户端超时，并假设请求丢失。
        /// </summary>
        [Description("WebDAV请求可能包含许多涉及文件操作的子请求，需要很长时间才能完成请求。该代码表示服务器已经收到并正在处理请求，但无响应可用。[5]这样可以防止客户端超时，并假设请求丢失。")]
        Processing = 102,
        /// <summary>
        /// 用来在最终的HTTP消息之前返回一些响应头。
        /// </summary>
        [Description("用来在最终的HTTP消息之前返回一些响应头。")]
        EarlyHints = 103,
        #endregion

        #region 2xx成功 这一类型的状态码，代表请求已成功被服务器接收、理解、并接受。
        /// <summary>
        /// 请求(或处理)成功
        /// </summary>
        [Description("请求(或处理)成功")]
        OK = 200,
        /// <summary>
        /// 请求已经被实现，而且有一个新的资源已经依据请求的需要而创建，且其URI已经随Location头信息返回。
        /// </summary>
        [Description("请求已经被实现，而且有一个新的资源已经依据请求的需要而创建，且其URI已经随Location头信息返回。")]
        Created = 201,
        /// <summary>
        /// 
        /// </summary>
        [Description()]
        Accepted = 202,
        /// <summary>
        /// 服务器是一个转换代理服务器（transforming proxy，例如网络加速器），以200 OK状态码为起源，但回应了原始响应的修改版本。
        /// </summary>
        [Description("服务器是一个转换代理服务器（transforming proxy，例如网络加速器），以200 OK状态码为起源，但回应了原始响应的修改版本。")]
        NonAuthoritativeInformation = 203,
        /// <summary>
        /// 服务器成功处理了请求，没有返回任何内容。
        /// </summary>
        [Description("服务器成功处理了请求，没有返回任何内容。")]
        NoContent = 204,
        /// <summary>
        /// 服务器成功处理了请求，但没有返回任何内容。与204响应不同，此响应要求请求者重置文档视图。
        /// </summary>
        [Description("服务器成功处理了请求，但没有返回任何内容。与204响应不同，此响应要求请求者重置文档视图。")]
        ResetContent = 205,
        /// <summary>
        /// 服务器已经成功处理了部分GET请求。类似于FlashGet或者迅雷这类的HTTP下载工具都是使用此类响应实现断点续传或者将一个大文档分解为多个下载段同时下载。
        /// </summary>
        [Description("服务器已经成功处理了部分GET请求。类似于FlashGet或者迅雷这类的HTTP下载工具都是使用此类响应实现断点续传或者将一个大文档分解为多个下载段同时下载。")]
        PartialContent = 206,
        /// <summary>
        /// 代表之后的消息体将是一个XML消息，并且可能依照之前子请求数量的不同，包含一系列独立的响应代码。
        /// </summary>
        [Description("代表之后的消息体将是一个XML消息，并且可能依照之前子请求数量的不同，包含一系列独立的响应代码。")]
        MultiStatus = 207,
        /// <summary>
        /// DAV绑定的成员已经在（多状态）响应之前的部分被列举，且未被再次包含。
        /// </summary>
        [Description("DAV绑定的成员已经在（多状态）响应之前的部分被列举，且未被再次包含。")]
        AlreadyReported = 208,
        /// <summary>
        /// 服务器已经满足了对资源的请求，对实体请求的一个或多个实体操作的结果表示。
        /// </summary>
        [Description("服务器已经满足了对资源的请求，对实体请求的一个或多个实体操作的结果表示。")]
        IMUsed = 226,
        #endregion

        #region 3xx重定向 这类状态码代表需要客户端采取进一步的操作才能完成请求。通常，这些状态码用来重定向，后续的请求地址（重定向目标）在本次响应的Location域中指明。
        /// <summary>
        /// 被请求的资源有一系列可供选择的回馈信息，每个都有自己特定的地址和浏览器驱动的商议信息。用户或浏览器能够自行选择一个首选的地址进行重定向。
        /// </summary>
        [Description("被请求的资源有一系列可供选择的回馈信息，每个都有自己特定的地址和浏览器驱动的商议信息。用户或浏览器能够自行选择一个首选的地址进行重定向。")]
        MultipleChoices = 300,
        /// <summary>
        /// 被请求的资源已永久移动到新位置，并且将来任何对此资源的引用都应该使用本响应返回的若干个URI之一。如果可能，拥有链接编辑功能的客户端应当自动把请求的地址修改为从服务器反馈回来的地址。除非额外指定，否则这个响应也是可缓存的。
        /// </summary>
        [Description("被请求的资源已永久移动到新位置，并且将来任何对此资源的引用都应该使用本响应返回的若干个URI之一。如果可能，拥有链接编辑功能的客户端应当自动把请求的地址修改为从服务器反馈回来的地址。除非额外指定，否则这个响应也是可缓存的。")]
        MovedPermanently = 301,
        /// <summary>
        /// 要求客户端执行临时重定向（原始描述短语为“Moved Temporarily”）。由于这样的重定向是临时的，客户端应当继续向原有地址发送以后的请求。只有在Cache-Control或Expires中进行了指定的情况下，这个响应才是可缓存的。
        /// </summary>
        [Description("要求客户端执行临时重定向（原始描述短语为“Moved Temporarily”）。由于这样的重定向是临时的，客户端应当继续向原有地址发送以后的请求。只有在Cache-Control或Expires中进行了指定的情况下，这个响应才是可缓存的。")]
        Found = 302,
        /// <summary>
        /// 对应当前请求的响应可以在另一个URI上被找到，当响应于POST（或PUT / DELETE）接收到响应时，客户端应该假定服务器已经收到数据，并且应该使用单独的GET消息发出重定向。
        /// </summary>
        [Description("对应当前请求的响应可以在另一个URI上被找到，当响应于POST（或PUT / DELETE）接收到响应时，客户端应该假定服务器已经收到数据，并且应该使用单独的GET消息发出重定向。")]
        SeeOther = 303,
        /// <summary>
        /// 表示资源在由请求头中的If-Modified-Since或If-None-Match参数指定的这一版本之后，未曾被修改。在这种情况下，由于客户端仍然具有以前下载的副本，因此不需要重新传输资源。
        /// </summary>
        [Description("表示资源在由请求头中的If-Modified-Since或If-None-Match参数指定的这一版本之后，未曾被修改。在这种情况下，由于客户端仍然具有以前下载的副本，因此不需要重新传输资源。")]
        NotModified = 304,
        /// <summary>
        /// 被请求的资源必须通过指定的代理才能被访问。Location域中将给出指定的代理所在的URI信息，接收者需要重复发送一个单独的请求，通过这个代理才能访问相应资源。只有原始服务器才能创建305响应。
        /// </summary>
        [Description("被请求的资源必须通过指定的代理才能被访问。Location域中将给出指定的代理所在的URI信息，接收者需要重复发送一个单独的请求，通过这个代理才能访问相应资源。只有原始服务器才能创建305响应。")]
        UseProxy = 305,
        /// <summary>
        /// 在最新版的规范中，306状态码已经不再被使用。最初是指“后续请求应使用指定的代理”。
        /// </summary>
        [Description("在最新版的规范中，306状态码已经不再被使用。最初是指“后续请求应使用指定的代理”。")]
        SwitchProxy = 306,
        /// <summary>
        /// 在这种情况下，请求应该与另一个URI重复，但后续的请求应仍使用原始的URI。
        /// </summary>
        [Description("在这种情况下，请求应该与另一个URI重复，但后续的请求应仍使用原始的URI。")]
        TemporaryRedirect = 307,
        /// <summary>
        /// 请求和所有将来的请求应该使用另一个URI重复。
        /// </summary>
        [Description("请求和所有将来的请求应该使用另一个URI重复。")]
        PermanentRedirect = 308,
        #endregion

        #region 4xx客户端错误 这类的状态码代表了客户端看起来可能发生了错误，妨碍了服务器的处理。除非响应的是一个HEAD请求，否则服务器就应该返回一个解释当前错误状况的实体，以及这是临时的还是永久性的状况。这些状态码适用于任何请求方法。浏览器应当向用户显示任何包含在此类错误响应中的实体内容。

        /// <summary>
        /// 由于明显的客户端错误（例如，格式错误的请求语法，太大的大小，无效的请求消息或欺骗性路由请求），服务器不能或不会处理该请求。
        /// </summary>
        [Description("由于明显的客户端错误（例如，格式错误的请求语法，太大的大小，无效的请求消息或欺骗性路由请求），服务器不能或不会处理该请求。")]
        BadRequest = 400,
        /// <summary>
        /// 类似于403 Forbidden，401语义即“未认证”，即用户没有必要的凭据。
        /// 该状态码表示当前请求需要用户验证。
        /// 该响应必须包含一个适用于被请求资源的WWW-Authenticate信息头用以询问用户信息。
        /// 客户端可以重复提交一个包含恰当的Authorization头信息的请求。
        /// 如果当前请求已经包含了Authorization证书，那么401响应代表着服务器验证已经拒绝了那些证书。
        /// 如果401响应包含了与前一个响应相同的身份验证询问，且浏览器已经至少尝试了一次验证，那么浏览器应当向用户展示响应中包含的实体信息，因为这个实体信息中可能包含了相关诊断信息。
        /// </summary>
        [Description("用户没有必要的凭据")]
        Unauthorized = 401,
        /// <summary>
        /// 该状态码是为了将来可能的需求而预留的。该状态码最初的意图可能被用作某种形式的数字现金或在线支付方案的一部分，但几乎没有哪家服务商使用，而且这个状态码通常不被使用。
        /// </summary>
        [Description("该状态码是为了将来可能的需求而预留的。该状态码最初的意图可能被用作某种形式的数字现金或在线支付方案的一部分，但几乎没有哪家服务商使用，而且这个状态码通常不被使用。")]
        PaymentRequired = 402,
        /// <summary>
        /// 服务器已经理解请求，但是拒绝执行它。
        /// </summary>
        [Description("服务器已经理解请求，但是拒绝执行它。")]
        Forbidden = 403,
        /// <summary>
        /// 请求失败，请求所希望得到的资源未被在服务器上发现，但允许用户的后续请求。
        /// </summary>
        [Description("请求失败，请求所希望得到的资源未被在服务器上发现，但允许用户的后续请求。")]
        NotFound = 404,
        /// <summary>
        /// 请求行中指定的请求方法不能被用于请求相应的资源。该响应必须返回一个Allow头信息用以表示出当前资源能够接受的请求方法的列表。
        /// </summary>
        [Description("请求行中指定的请求方法不能被用于请求相应的资源。该响应必须返回一个Allow头信息用以表示出当前资源能够接受的请求方法的列表。")]
        MethodNotAllowed = 405,
        /// <summary>
        /// 请求的资源的内容特性无法满足请求头中的条件，因而无法生成响应实体，该请求不可接受。
        /// </summary>
        [Description("请求的资源的内容特性无法满足请求头中的条件，因而无法生成响应实体，该请求不可接受。")]
        NotAcceptable = 406,
        /// <summary>
        /// 与401响应类似，只不过客户端必须在代理服务器上进行身份验证。
        /// </summary>
        [Description("与401响应类似，只不过客户端必须在代理服务器上进行身份验证。")]
        ProxyAuthenticationRequired = 407,
        /// <summary>
        /// 请求超时。根据HTTP规范，客户端没有在服务器预备等待的时间内完成一个请求的发送，客户端可以随时再次提交这一请求而无需进行任何更改。
        /// </summary>
        [Description("请求超时。根据HTTP规范，客户端没有在服务器预备等待的时间内完成一个请求的发送，客户端可以随时再次提交这一请求而无需进行任何更改。")]
        RequestTimeout = 408,
        /// <summary>
        /// 表示因为请求存在冲突无法处理该请求，例如多个同步更新之间的编辑冲突。
        /// </summary>
        [Description("表示因为请求存在冲突无法处理该请求，例如多个同步更新之间的编辑冲突。")]
        Conflict = 409,
        /// <summary>
        /// 表示所请求的资源不再可用，将不再可用。当资源被有意地删除并且资源应被清除时，应该使用这个。在收到410状态码后，用户应停止再次请求资源。
        /// </summary>
        [Description("表示所请求的资源不再可用，将不再可用。当资源被有意地删除并且资源应被清除时，应该使用这个。在收到410状态码后，用户应停止再次请求资源。")]
        Gone = 410,
        /// <summary>
        /// 服务器拒绝在没有定义Content-Length头的情况下接受请求。在添加了表明请求消息体长度的有效Content-Length头之后，客户端可以再次提交该请求。
        /// </summary>
        [Description("服务器拒绝在没有定义Content-Length头的情况下接受请求。在添加了表明请求消息体长度的有效Content-Length头之后，客户端可以再次提交该请求。")]
        LengthRequired = 411,
        /// <summary>
        /// 服务器在验证在请求的头字段中给出先决条件时，没能满足其中的一个或多个。这个状态码允许客户端在获取资源时在请求的元信息（请求头字段数据）中设置先决条件，以此避免该请求方法被应用到其希望的内容以外的资源上。
        /// </summary>
        [Description("服务器在验证在请求的头字段中给出先决条件时，没能满足其中的一个或多个。这个状态码允许客户端在获取资源时在请求的元信息（请求头字段数据）中设置先决条件，以此避免该请求方法被应用到其希望的内容以外的资源上。")]
        PreconditionFailed = 412,
        /// <summary>
        /// 前称“Request Entity Too Large”，表示服务器拒绝处理当前请求，因为该请求提交的实体数据大小超过了服务器愿意或者能够处理的范围。此种情况下，服务器可以关闭连接以免客户端继续发送此请求。
        /// </summary>
        [Description("前称“Request Entity Too Large”，表示服务器拒绝处理当前请求，因为该请求提交的实体数据大小超过了服务器愿意或者能够处理的范围。此种情况下，服务器可以关闭连接以免客户端继续发送此请求。")]
        RequestEntityTooLarge = 413,
        /// <summary>
        /// 前称“Request-URI Too Long”，表示请求的URI长度超过了服务器能够解释的长度，因此服务器拒绝对该请求提供服务。通常将太多数据的结果编码为GET请求的查询字符串，在这种情况下，应将其转换为POST请求。
        /// </summary>
        [Description("前称“Request-URI Too Long”，表示请求的URI长度超过了服务器能够解释的长度，因此服务器拒绝对该请求提供服务。通常将太多数据的结果编码为GET请求的查询字符串，在这种情况下，应将其转换为POST请求。")]
        RequestURITooLong = 414,
        /// <summary>
        /// 对于当前请求的方法和所请求的资源，请求中提交的互联网媒体类型并不是服务器中所支持的格式，因此请求被拒绝。
        /// </summary>
        [Description("对于当前请求的方法和所请求的资源，请求中提交的互联网媒体类型并不是服务器中所支持的格式，因此请求被拒绝。")]
        UnsupportedMediaType = 415,
        /// <summary>
        /// 前称“Requested Range Not Satisfiable”。[45]客户端已经要求文件的一部分（Byte serving），但服务器不能提供该部分。
        /// </summary>
        [Description("前称“Requested Range Not Satisfiable”。客户端已经要求文件的一部分（Byte serving），但服务器不能提供该部分。")]
        RequestedRangeNotSatisfiable = 416,
        /// <summary>
        /// 在请求头Expect中指定的预期内容无法被服务器满足，或者这个服务器是一个代理服显的证据证明在当前路由的下一个节点上，Expect的内容无法被满足。
        /// </summary>
        [Description("在请求头Expect中指定的预期内容无法被服务器满足，或者这个服务器是一个代理服显的证据证明在当前路由的下一个节点上，Expect的内容无法被满足。")]
        ExpectationFailed = 417,
        /// <summary>
        /// 本操作码是在1998年作为IETF的传统愚人节笑话, 在RFC 2324超文本咖啡壶控制协议'中定义的，并不需要在真实的HTTP服务器中定义。
        /// </summary>
        [Description("本操作码是在1998年作为IETF的传统愚人节笑话, 在RFC 2324超文本咖啡壶控制协议'中定义的，并不需要在真实的HTTP服务器中定义。")]
        ImATeapot = 418,
        /// <summary>
        /// 该请求针对的是无法产生响应的服务器（例如因为连接重用）。
        /// </summary>
        [Description("该请求针对的是无法产生响应的服务器（例如因为连接重用）。")]
        MisdirectedRequest = 421,
        /// <summary>
        /// 请求格式正确，但是由于含有语义错误，无法响应。
        /// </summary>
        [Description("请求格式正确，但是由于含有语义错误，无法响应。")]
        UnprocessableEntity = 422,
        /// <summary>
        /// 当前资源被锁定。
        /// </summary>
        [Description("当前资源被锁定。")]
        Locked = 423,
        /// <summary>
        /// 由于之前的某个请求发生的错误，导致当前请求失败，例如PROPPATCH。
        /// </summary>
        [Description("由于之前的某个请求发生的错误，导致当前请求失败，例如PROPPATCH。")]
        FailedDependency = 424,
        /// <summary>
        /// 服务器拒绝处理在Early Data中的请求，以规避可能的重放攻击。
        /// </summary>
        [Description("服务器拒绝处理在Early Data中的请求，以规避可能的重放攻击。")]
        TooEarly = 425,
        /// <summary>
        /// 客户端应切换到Upgrade头字段中给出的不同协议，如TLS/1.0。
        /// </summary>
        [Description("客户端应切换到Upgrade头字段中给出的不同协议，如TLS/1.0。")]
        UpgradeRequired = 426,
        /// <summary>
        /// 原服务器要求该请求满足一定条件。这是为了防止“未更新”问题，即客户端读取（GET）一个资源的状态，更改它，并将它写（PUT）回服务器，但这期间第三方已经在服务器上更改了该资源的状态，因此导致了冲突。
        /// </summary>
        [Description("原服务器要求该请求满足一定条件。这是为了防止“未更新”问题，即客户端读取（GET）一个资源的状态，更改它，并将它写（PUT）回服务器，但这期间第三方已经在服务器上更改了该资源的状态，因此导致了冲突。")]
        PreconditionRequired = 428,
        /// <summary>
        /// 用户在给定的时间内发送了太多的请求。旨在用于网络限速。
        /// </summary>
        [Description("用户在给定的时间内发送了太多的请求。旨在用于网络限速。")]
        TooManyRequests = 429,
        /// <summary>
        /// 服务器不愿处理请求，因为一个或多个头字段过大。
        /// </summary>
        [Description("服务器不愿处理请求，因为一个或多个头字段过大。")]
        RequestHeaderFieldsTooLarge = 431,
        /// <summary>
        /// 该访问因法律的要求而被拒绝，由IETF在2015核准后新增加。
        /// </summary>
        [Description("该访问因法律的要求而被拒绝，由IETF在2015核准后新增加。")]
        UnavailableForLegalReasons = 451,
        #endregion

        #region 5xx服务器错误 表示服务器无法完成明显有效的请求。[56]这类状态码代表了服务器在处理请求的过程中有错误或者异常状态发生，也有可能是服务器意识到以当前的软硬件资源无法完成对请求的处理。除非这是一个HEAD请求，否则服务器应当包含一个解释当前错误状态以及这个状况是临时的还是永久的解释信息实体。浏览器应当向用户展示任何在当前响应中被包含的实体。这些状态码适用于任何响应方法
        /// <summary>
        /// 通用错误消息，服务器遇到了一个未曾预料的状况，导致了它无法完成对请求的处理。没有给出具体错误信息。
        /// </summary>
        [Description("通用错误消息，服务器遇到了一个未曾预料的状况，导致了它无法完成对请求的处理。没有给出具体错误信息。")]
        InternalServerError = 500,
        /// <summary>
        /// 服务器不支持当前请求所需要的某个功能。当服务器无法识别请求的方法，并且无法支持其对任何资源的请求。
        /// </summary>
        [Description("服务器不支持当前请求所需要的某个功能。当服务器无法识别请求的方法，并且无法支持其对任何资源的请求。")]
        NotImplemented = 501,
        /// <summary>
        /// 作为网关或者代理工作的服务器尝试执行请求时，从上游服务器接收到无效的响应。
        /// </summary>
        [Description("作为网关或者代理工作的服务器尝试执行请求时，从上游服务器接收到无效的响应。")]
        BadGateway = 502,
        /// <summary>
        /// 由于临时的服务器维护或者过载，服务器当前无法处理请求。这个状况是暂时的，并且将在一段时间以后恢复。
        /// </summary>
        [Description("由于临时的服务器维护或者过载，服务器当前无法处理请求。这个状况是暂时的，并且将在一段时间以后恢复。")]
        ServiceUnavailable = 503,
        /// <summary>
        /// 作为网关或者代理工作的服务器尝试执行请求时，未能及时从上游服务器（URI标识出的服务器，例如HTTP、FTP、LDAP）或者辅助服务器（例如DNS）收到响应。
        /// </summary>
        [Description("作为网关或者代理工作的服务器尝试执行请求时，未能及时从上游服务器（URI标识出的服务器，例如HTTP、FTP、LDAP）或者辅助服务器（例如DNS）收到响应。")]
        GatewayTimeout = 504,
        /// <summary>
        /// 服务器不支持，或者拒绝支持在请求中使用的HTTP版本。
        /// </summary>
        [Description("服务器不支持，或者拒绝支持在请求中使用的HTTP版本。")]
        HTTPVersionNotSupported = 505,
        /// <summary>
        /// 由《透明内容协商协议》（RFC 2295）扩展，代表服务器存在内部配置错误，[64]被请求的协商变元资源被配置为在透明内容协商中使用自己，因此在一个协商处理中不是一个合适的重点。
        /// </summary>
        [Description("由《透明内容协商协议》（RFC 2295）扩展，代表服务器存在内部配置错误，[64]被请求的协商变元资源被配置为在透明内容协商中使用自己，因此在一个协商处理中不是一个合适的重点。")]
        VariantAlsoNegotiates = 506,
        /// <summary>
        /// 服务器无法存储完成请求所必须的内容。这个状况被认为是临时的。
        /// </summary>
        [Description("服务器无法存储完成请求所必须的内容。这个状况被认为是临时的。")]
        InsufficientStorage = 507,
        /// <summary>
        /// 服务器在处理请求时陷入死循环。
        /// </summary>
        [Description("服务器在处理请求时陷入死循环。")]
        LoopDetected = 508,
        /// <summary>
        /// 获取资源所需要的策略并没有被满足。
        /// </summary>
        [Description("获取资源所需要的策略并没有被满足。")]
        NotExtended = 510,
        /// <summary>
        /// 客户端需要进行身份验证才能获得网络访问权限，旨在限制用户群访问特定网络。
        /// </summary>
        [Description("客户端需要进行身份验证才能获得网络访问权限，旨在限制用户群访问特定网络。")]
        NetworkAuthenticationRequired = 511,
        #endregion
    }
}
