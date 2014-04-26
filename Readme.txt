Gyrasoft fork of the NVelocity engine as of Castle.NVelocity 1.1.1
  
  To differentiate project, I changed solution and project name to NuVelocity,
  but namespaces and assembly names are original NVelocity for drop-in
  compatibility.
  
  NVelocity is Velocity for .NET. It is a port of the Apache Velocity engine for Java.

  NVelocity, in its various stages / forks, can be found across the internet on all major
  source control sites. The most recent, mature fork, at the time of my fork, was
  Castle.NVelocity 1.1.1 within the Castle project. Not to be confused with the CodePlex
  fork, which has higher release numbers, but is certainly not the most mature, and has
  more original bugs. I can go into those issues, but I won't. The Castle port is the
  de facto NVelocity at this time, and if you want something other than my fork, I
  recommend you avoid the CodePlex fork. Castle.NVelocity is available via Nuget (I have
  not yet submitted my for through Nuget yet).

  You will find many Java-isms in the NVelocity source code. Some concepts don't even
  have counterparts across the languages. I don't feel that it would be wise to spend
  time refactoring just to remove Java-isms, unless it is justified by other reasons.

  Because of things like MVC4 and Razor, there is less use of NVelocity on new sites,
  however, Velocity is still a very powerful, widely used template engine that is
  worth maintence due to its roots and counterpart in the Java world. I have commercial
  projects that depend on Velocity on .NET, and I plan to fix bugs and improve performance
  as needed. Not only that, I wish to potentially evolve it a bit, and don't wish to worry
  about backwards compatibility. 
  
  If you have interest in contribution to this project, and I am not moving fast enough
  for you, notify me for commit privs.

  The Castle Project has been inactive for a couple of years, as has NVelocity. I have no
  reason to wish to maintain a separate project, and hopefully any fixes I make to the
  library will make their way back. However, I also have no time to figure out
  who/where/when to contact for patches, as I focus on commercial work nowadays, so I have
  simply made my fork public for anyone who wishes to use it, or to contribute, as long as
  test cases pass and stability and code quality is maintained.
 
  Thanks to the original Authors, and the Castle guys, for this library. I claim no credit
  for anything except for fixing bugs and hopefully preserving a great engine.
  
  Melvin Smith (melvin.smith@gyrasoft.com) - April 25, 2014
  