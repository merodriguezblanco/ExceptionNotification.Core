# ExceptionNotification.Core

## Overview

`ExceptionNotification.Core` is a NET core package that provides a set of notifiers for sending exception notifications when errors occur in your NET Core API. So far, the notifiers can deliver notifications only via e-mail. Its idea is based on the great [Exception Notification gem](https://github.com/smartinez87/exception_notification) that provides notifiers for Ruby applications.

## WARNING: This plugin is in early development stage.


## Requirements

* NET Core 2.0 SDK

## Installation

Install this package using the NuGet command line:

```bash
PM> Install-Package ExceptionNotification.Core -Version 1.0.0
```

## Usage

`ExceptionNotification.Core` provides a middleware that catches exceptions for API requests. It also provides an interface to manually send notifications from a background process.

To setup the package you must add some credentials to your `appsettings.<environment>.json` file:

```json
{
  // ...

  "ExceptionEmailConfiguration": {
    "SmtpServer": "your.server.com",
    "SmtpPort": "25",
    "SmtpUser": "username",
    "SmtpPassword": "password",
    "EnableSsl": "true,
    "UseCredentials": true,
    "Sender": {
      "DisplayName": "John Doe",
      "Address": "johndoe@server.com"
    },
    "Recipients": {
      "DisplayName": "Mary",
      "Address": "mary@test.com"
    }
  }
}
```

The `UseCredentials` option defaults to `true`. It can be set to `false` in case that you are using a relay server to send e-mails. In such case, you don't need to specify the `SmtpUser` and `SmtpPassword`.

The package initialization should be setup in the `Startup.cs`:

```csharp
public class Startup
{
  // ...

  public void ConfigureServices(IServiceCollection services)
  {
    // ...

    // Bind e-mail configuration from appsettings file.
    var emailConfiguration = new EmailConfiguration();
    Configuration.Bind("ExceptionEmailConfiguration", emailConfiguration);

    EmailExceptionNotifier.Setup(emailConfiguration);

    // Add singleton service to be used by the middleware.
    services.AddSingleton<IEmailExceptionNotifier>(emailConfiguration);
  }

  public void Configure(IApplicationBuilder app)
  {
    // ...

    app.UseMiddleware<EmailExceptionMiddleware>();
  }
}
```


## Background process notifications

In case that you want to send notifications from a background process, you can make use of the `EmailExceptionNotification` interface:

```csharp
try
{
  // ...
}
catch(Exception exception)
{
  EmailExceptionNotification.NotifyException(exception);
}
```

## TODO

This package currently provides an e-mail notifier. It would be ideal to implement the following notifiers as well:

* Slack
* Hipchat

## Contributing

We encourage you to contribute to **ExceptionNotification.Core** by following the [CONTRIBUTING](CONTRIBUTING.md) instructions.

## License

This package is available as open source under the [MIT license](https://www.opensource.org/licenses/MIT).
