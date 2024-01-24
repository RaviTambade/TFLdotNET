# Service LifeTime

Service lifetime in a Dependency Injection (DI) container refers to the duration for which an instance of a registered service is 𝐤𝐞𝐩𝐭 𝐢𝐧 𝐦𝐞𝐦𝐨𝐫𝐲.

𝐃𝐢𝐟𝐟𝐞𝐫𝐞𝐧𝐭 𝐥𝐢𝐟𝐞𝐭𝐢𝐦𝐞𝐬 serve different purposes, and choosing the appropriate lifetime is essential for ensuring that your application behaves as expected and efficiently manages resources.

There are three common service lifetimes:

𝐓𝐫𝐚𝐧𝐬𝐢𝐞𝐧𝐭 𝐋𝐢𝐟𝐞𝐭𝐢𝐦𝐞: A new instance of the service is created every time it is requested.

𝐒𝐜𝐨𝐩𝐞𝐝 𝐋𝐢𝐟𝐞𝐭𝐢𝐦𝐞: single instance of the service is created and shared within the scope of a single operation or request. In the context of a web application, this typically corresponds to the duration of an HTTP request.

𝐒𝐢𝐧𝐠𝐥𝐞𝐭𝐨𝐧 𝐋𝐢𝐟𝐞𝐭𝐢𝐦𝐞: A single instance of the service is created and shared across all requests. This means that the same instance is reused every time the service is requested.