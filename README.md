# ExceptionNotification.Core

[![NuGet](https://img.shields.io/nuget/v/ExceptionNotification.Core.svg?style=flat-square)](https://www.nuget.org/packages/ExceptionNotification.Core)
[![Build Status](https://travis-ci.com/merodriguezblanco/ExceptionNotification.Core.svg?branch=master)](https://travis-ci.com/merodriguezblanco/ExceptionNotification.Core)

## Overview

`ExceptionNotification.Core` is a NET core package that provides a set of notifiers for sending exception notifications when errors occur in your NET Core API. So far, the notifiers can deliver notifications only via e-mail, HipChat, and Slack. Its idea is based on the great [ExceptionNotification gem](https://github.com/smartinez87/exception_notification) that provides notifiers for Ruby applications.

## **WARNING: This plugin is in early development stage.**


## Requirements

* NET Core 2.0 SDK

## Installation

Install this package using the NuGet command line:

```bash
PM> Install-Package ExceptionNotification.Core -Version 1.4.0
```

## Usage

`ExceptionNotification.Core` provides a middleware that catches exceptions for API requests. It also provides an interface to manually send notifications from a background process.

To setup the package you must add some credentials to your `appsettings.<environment>.json` file:

```json
{
  "ExceptionNotification": {
    "Email": {
      "SmtpServer": "your.server.com",
      "SmtpPort": "25",
      "SmtpUser": "username",
      "SmtpPassword": "password",
      "EnableSsl": true,
      "UseCredentials": true,
      "Sender": {
        "DisplayName": "John Doe",
        "Address": "johndoe@server.com"
      },
      "Recipients": {
        "DisplayName": "Mary",
        "Address": "mary@test.com"
      }
    },
    "Hipchat": {
      "RoomName": "Your Room",
      "ApiToken": "D12@....."
    },
    "Slack": {
      "WebhookUri": "https://...",
      "Channel": "#channel-name",
      "Username": "aperson"
    }
  }
}
```

### Email
* `SmtpServer` - *required* - Specifies the remote e-mail server address.
* `SmtpPort` - *required* - Port used by the e-mail server.
* `SmtpUser` - *optional* - If your e-mail server requires authentication, this setting specifies the username.
* `SmtpPassword` - *optional* - If your e-mail server requires authentication, this setting specifies the password.
* `EnableSsl` - *optional*.
* `UseCredentials`- *optional* - Defaults to `true`. It can be set to `false` in case that you are using a relay server to send e-mails. In such case, you don't need to specify the `SmtpUser` and `SmtpPassword`.
* `Sender`- *required* - Specifices who the notification message is from.
* `Recipients`- *required* - List of recipients that will receive the notification message.

### HipChat
* `RoomName` - *required* - Specifies the HipcHat's room name where the notification message will be published to.
* `ApiToken`- *required* - The [API token](https://www.hipchat.com/docs/apiv2/method/generate_token) that allows access to your HipChat account.

### Slack Options
* `WebhookUri` - *required* - The incoming [Webhook URI](https://api.slack.com/incoming-webhooks) on Slack.
* `Channel` - *optional* - Channel's name in which the message will appear.
* `Username` - *optional* - Username of the bot.


The package initialization should be setup in the `Startup.cs`:

```csharp
public class Startup
{
  // ...

  public void ConfigureServices(IServiceCollection services)
  {
    // ...

    // Bind configuration from appsettings file.
    var configuration = new ExceptionNotificationConfiguration();
    Configuration.Bind("ExceptionNotification", configuration);

    ExceptionNotifier.Setup(configuration);

    // Add singleton service to be used by the middleware.
    services.AddSingleton<IExceptionNotificationConfiguration>(configuration);
  }

  public void Configure(IApplicationBuilder app)
  {
    // ...

    app.UseMiddleware<ExceptionMiddleware>();
  }
}
```


## Background process notifications

In case that you want to send notifications from a background process, you can make use of the `ExceptionNotifier` interface:

```csharp
try
{
  // ...
}
catch(Exception exception)
{
  ExceptionNotifier.NotifyException(exception);
}
```

## TODO

This package currently provides e-mail, HipChat, and Slack notifiers. It would be ideal to implement other notifiers as well.
More testing is also needed.

## Contributing

We encourage you to contribute to **ExceptionNotification.Core** by following the [CONTRIBUTING](CONTRIBUTING.md) instructions.

## License

This package is available as open source under the [MIT license](https://www.opensource.org/licenses/MIT).
