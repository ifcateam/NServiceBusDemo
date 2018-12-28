---
title: Sending from an ASP.NET MVC Controller
summary: Leverages Autofac MVC integration to inject an endpoint instance into MVC Controllers.
component: Core
reviewed: 2018-03-12
redirects:
- nservicebus/injecting-the-bus-into-asp.net-mvc-controller
- samples/web/asp-mvc-injecting-bus
related:
- nservicebus/dependency-injection
- nservicebus/hosting
- nservicebus/hosting/publishing-from-web-applications
---

The sample demonstrates how to inject an endpoint instance info MVC controllers using Autofac's MVC integration.

### Wire up Autofac

Open `Global.asax.cs` and look at the `ApplicationStart` method.

snippet: ApplicationStart


### Injection into the Controller

The endpoint instance is injected into the `DefaultController` at construction time.

snippet: Controller