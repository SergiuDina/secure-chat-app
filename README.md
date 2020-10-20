# Secure Chat App

In this project I created an app for secure communication, using a client-server architecture. The programming language used for this project is C#.

In order to create the app, I used different .NET frameworks, such as WCF, WPF and EF.

The communication between server and client is of type TCP full duplex.

The communication is secured using AES (Advanced Enryption Standard). For the key exchange between clients I used Diffie-Hellman with elliptic curves, more precisely Curve25519.

## Usage

Before you run the application:

Right Click on the Solution and Restore NuGetPackages to make sure the app has all the package dependencies that it needs.

Right Click on the Solution, go to Properties, Common Properties, Startup Project, select Multiple startup projects and select the option "Start" for ServerHost and Client, this will make sure that when you run the application, both the server and a client instance will open.

After you run the application:

In the application folder, go to Client\bin\Debug and open Client.exe to open another client

Now you can Login or Register two new clients, connect to the Chat Room and start sending messages between them.

## App Design

The design for the client user interface was created using WPF

Here is the Login Page:

![5](https://user-images.githubusercontent.com/70022000/96606636-28c72680-1300-11eb-99dc-f49d4378f7fa.jpeg)

This is the Chat Room:

![13](https://user-images.githubusercontent.com/70022000/96606758-4c8a6c80-1300-11eb-8283-47e91fe812b6.jpeg)

And here is the Server, that also acts as a log where you can see: users logging in or out, registering, the key echange between two users that happens when they start a conversation (the key is only usable for the current session) and the encrypted messages.

![16](https://user-images.githubusercontent.com/70022000/96606871-6af06800-1300-11eb-86c9-67f9ae5ac3b0.jpeg)

