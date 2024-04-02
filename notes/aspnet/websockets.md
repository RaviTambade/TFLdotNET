## WebSockets

WebSocket is a realtime technology that enables bidirectional, full-duplex communication between client and server over a persistent, single-socket connection. The WebSocket connection is kept alive for as long as needed (in theory, it can last forever), allowing the server and the client to send data at will, with minimal overhead.

The first realtime web apps started to appear in the 2000s, attempting to deliver responsive, dynamic, and interactive end-user experiences. However, at that time, the realtime web was difficult to achieve and slower than we’re used to nowadays; it was delivered by hacking existing HTTP-based technologies (AJAX and Comet) that were not designed and optimized for realtime applications. It quickly became obvious that a better alternative was needed.

In 2008, the pain and limitations of using AJAX and Comet when implementing anything resembling realtime were being felt particularly keenly by developers Michael Carter and Ian Hickson. Through collaboration on IRC and W3C mailing lists, they came up with a plan to introduce a new standard for modern, truly realtime communication on the web. Thus, the name “WebSocket’’ was coined.


## What is the WebSocket protocol?
The WebSocket protocol enables ongoing, full-duplex, bidirectional communication between a web client and a web server over an underlying TCP connection. The protocol is designed to allow clients and servers to communicate in realtime, allowing for efficient and responsive data transfer in web applications.

In December 2011, the Internet Engineering Task Force (IETF) standardized the WebSocket protocol through RFC 6455. In coordination with IETF, the Internet Assigned Numbers Authority (IANA) maintains the WebSocket Protocol Registries, which define many of the codes and parameter identifiers used by the protocol.


## How do WebSockets work?
In a nutshell, working with WebSockets involves three main steps:

1. <b>Opening a WebSocket connection.</b> The process of establishing a WebSocket connection is known as the opening handshake, and consists of an HTTP request/response exchange between the client and the server.

2. <b>Data transmission over WebSockets.</b> After a successful WebSocket handshake, the client and server can exchange messages (frames) over the persistent WebSocket connection. WebSocket messages may contain string (plain text) or binary data. 

3. <b>Closing a WebSocket connection.</b> Once the persistent WebSocket connection has served its purposes, it can be terminated; both the client and the server can initiate the closing handshake by sending a close message.


## What are the pros and cons of WebSockets?
The advantage of WebSockets is that they enable realtime communication between the client and server without the need for frequent HTTP requests/responses. This brings benefits such as reduced latency, and improved performance and responsiveness of web apps. 

Due to its persistent and bidirectional nature, the WebSocket protocol is more flexible than HTTP when building realtime apps that require frequent data exchanges. WebSockets are also more efficient, as they allow data to be transmitted without the need for repetitive HTTP headers and handshakes. This can reduce bandwidth usage and server load.

While WebSockets have plenty of advantages, they also come with some disadvantages. Here are the main ones:

- WebSockets are not optimized for streaming audio and video data.
- WebSockets don’t automatically recover when connections are terminated.
- Some environments (such as corporate networks with proxy servers) will block WebSocket connections.
- WebSockets are stateful, which makes them hard to use in large-scale systems.


## Are WebSockets scalable?
Yes, WebSockets are scalable. Companies like Slack, Netflix, and Uber use WebSockets to power realtime features in their apps for millions of end-users. For example, Slack uses WebSockets for instant messaging between chat users. 

However, scaling WebSockets is non-trivial, and involves numerous engineering decisions and technical trade-offs. Among them:

- Should you use vertical or horizontal scaling?
- How do you deal with unpredictable loads?
- How do you manage WebSocket connections at scale?
- How much bandwidth is being used overall, and how is it impacting your budget?
- Do you have to deal with traffic spikes, and if so, what is the performance impact on the server layer?
- How will you automatically add additional server capacity if and when it’s needed?
- How do you ensure data integrity (guaranteed message ordering and delivery) at scale?



## What are WebSockets used for? 
WebSockets offer low-latency communication capabilities which are suitable for various types of realtime use cases. For example, you can use WebSockets to: 
- Power live chat experiences. 
- Broadcast realtime event data, such as live scores and traffic updates.
- Facilitate multiplayer collaboration on shared projects and whiteboards.
- Deliver notifications and alerts.
- Keep your backend and frontend in realtime sync.
- Add live location tracking capabilities to urban mobility and food delivery apps.


## What are the best alternatives to WebSockets? 
WebSocket is an excellent choice for use cases where it’s critical (or at least desirable) for data to be sent and consumed in realtime or near-realtime. However, there is rarely a one-size-fits-all protocol — different protocols serve different purposes better than others. Realtime alternatives to WebSockets include:
- Server-Sent Events
- HTTP long polling
- MQTT
- WebRTC
- WebTransport

## How to start building realtime experiences with WebSockets
Getting started with WebSockets is straightforward. The WebSocket API is trivial to use, and there are numerous WebSocket libraries and frameworks available in every programming language. Most of them are built on top of the raw WebSocket protocol, while providing additional capabilities — thus making it easier and more convenient for developers to implement WebSockets into their apps and build WebSocket-based functionality.

## WebSockets FAQs 

### What is a WebSocket connection?
You can think of a WebSocket connection as a long-lived, bidirectional, full-duplex communication channel between a web client and a web server. Note that WebSocket connections work on top of TCP. 

### Are WebSockets secure?
WebSockets can be secure if they are implemented with appropriate security measures. Secure WebSocket connections use the "wss://" URI. This indicates that the connection is encrypted with SSL/TLS, which ensures that the data transmitted between the WebSocket client and WebSocket server is encrypted and cannot be intercepted or tampered with by third parties. 

Additionally, WebSocket connections can be subject to the same security policies as HTTP connections, such as cross-origin resource sharing (CORS) restrictions, which prevent unauthorized access to resources across different domains.

Note that the WebSocket protocol doesn’t prescribe any particular way for servers to authenticate clients. For example, you can handle authentication during the opening handshake, by using cookie headers. Another option is to manage authentication (and authorization) at the application level, by using techniques such as JSON Web Tokens.

### Are WebSockets faster than HTTP? 
In the context of realtime apps that require frequent data exchanges, WebSockets are faster than HTTP. 

HTTP connections have additional overhead, such as headers and other metadata, that can add latency and reduce performance compared to WebSocket connections, which are designed for persistent, low-latency, bidirectional communication. With WebSockets, there’s no need for multiple HTTP requests and responses. This can result in faster communication and lower latency.

### Are WebSockets synchronous or asynchronous?
WebSockets are asynchronous by design, meaning that data can be sent and received at any time, without blocking or waiting for a response. However, it's important to note that while WebSockets themselves are asynchronous, the code used to handle WebSocket events and messages may be synchronous or asynchronous, depending on how it’s written.

### Are WebSockets expensive?
A WebSocket connection is not inherently expensive, as it's designed to be lightweight and efficient, with minimal overhead. That being said,  building and managing a scalable and reliable WebSocket system in-house is expensive, time-consuming, and requires significant engineering effort:
- 10.2 person-months is the average time to build basic WebSocket infrastructure, with limited scalability, in-house.
- Half of all self-built WebSocket solutions require $100K-$200K a year in upkeep.

### What browsers support WebSockets?
WebSockets are supported by most modern web browsers, including:
- Google Chrome (version 4 and later).
- Mozilla Firefox (version 4 and later).
- Safari (version 5 and later).
- Microsoft Edge (version 12 and later).
- Opera (version 10.70 and later).
- Internet Explorer (version 10 and later).
- Microsoft Edge (version 12 and later). 
Note that older versions of these browsers either don’t support WebSockets, or have limited support. At the time of writing (25th of April 2023), Opera Mini is the only modern browser that doesn’t support WebSockets. 

### How long can a WebSocket stay open?
In general, WebSocket connections can stay open indefinitely as long as both the client and server remain connected and the network is stable. 

### Are WebSockets stateful or stateless?
Unlike HTTP, a WebSocket connection is persistent and stateful. This makes WebSockets hard to use in large-scale systems that consist of multiple WebSocket servers (you need to share connection state across servers).