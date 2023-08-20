// See https://aka.ms/new-console-template for more information
Console.WriteLine("The application has a message: Hello, World!");

#if X86
    Console.WriteLine("The application is running in x86 mode.");
#endif

#if X64
    Console.WriteLine("The application is running in x64 mode.");
#endif

#if DEBUG
    Console.WriteLine("The application is running in debug mode.");
#endif

#if RELEASE
    Console.WriteLine("The application is running in release mode.");
#endif