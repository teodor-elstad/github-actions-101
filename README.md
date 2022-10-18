GitHub Actions 101
==================
_Et begynnerkurs i GitHub Actions, hvor du lærer å skrive en CI/CD-pipeline som bygger, tester og deployer en applikasjon._

💡 Hva er dette for noe?
------------------------
Flere ønsker å lære mer om CI/CD-verktøy generelt, og [GitHub Actions](https://docs.github.com/en/actions/learn-github-actions) spesielt. Derfor er dette kurset laget, hvor man ser på CI/CD fra A til Å, med utgangspunkt i GitHub Actions.

Dette er et begynnerkurs i GitHub Actions, og passer for deg som har jobbet lite eller ingenting med dette fra før. Her starter du med en eksempelapplikasjon, og i løpet av 2 timer får du prøve deg på å skrive en CI/CD-pipeline i GitHub Actions som bygger, tester og deployer applikasjonen. I tillegg inneholder repoet en kort presentasjon av de viktigste temaene innen CI/CD generelt.

🚦Hvordan kommer jeg i gang?
----------------------------
Før du kan kjøre applikasjonen lokalt, trenger du å installere [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download), og et passende verktøy for å editere kode. Hvis du ikke har noen spesielle preferanser, er [Visual Studio Code](https://code.visualstudio.com/) et greit valg.

For å klone koden, trenger du [Git](https://git-scm.com/downloads). I tillegg trenger du en konto på [GitHub](https://github.com/join) for å kunne bruke [Actions](https://docs.github.com/en/actions/learn-github-actions).

Når vi kommer så langt i kurset at man skal begynne å installere applikasjonen i forskjellige miljøer, trenger du [Docker](https://docs.docker.com/get-docker/) eller [Podman](https://podman.io/getting-started/installation) for å bygge containere, og [kubectl](https://kubernetes.io/docs/tasks/tools/#kubectl) for å orkestrere containerne du bygger i Kubernetes.

🐙 Kort om CI/CD
----------------
For at applikasjonene vi lager skal kunne brukes av noen andre enn oss, må vi typisk få de ut et eller annet sted hvor noen andre enn oss kan bruke de. Dette andre stedet kaller vi gjerne produksjon, og på veien ut kan det skje mye rart.

![](Images/fra-utvikler-til-produksjon.png)

_**Spørsmål:** Hva må til for at en applikasjon vi har laget kommer seg ut i produksjon?_

### En ordliste for CI/CD
- **CI:** Kontinuering Integrering (Continuous Integration). Praksis hvor man forsøker å samle kode-endringer fra flere bidragsytere hyppig, ved å automatisk merge inn og teste små endringer kontinuerlig.
- **CD:** Kontinuerlig Leveranse (Continuous Delivery). Tilnærming hvor man blant annet ønsker å redusere risiko for alvorlige feil i produksjon, ved å levere hyppige små endringer.
- **CI/CD-pipeline:** Den automatiske prosessen kode må igjennom for å komme seg fra en utvikler til produksjon.
- **Ressurs:** Noe som applikasjonen vår er avhengig av for å kjøre. Noen typiske eksempler er: En datamaskin (eller tilsvarende) som kan kjøre den ferdige applikasjonen vår. Databaser, køer og filer for å holde på tilstand. Nettverk for å snakke med omverdenen. Andre applikasjoner som vår applikasjon bruker.
- **Delte ressurser:** Ressurser som brukes av andre applikasjoner enn den applikasjonen vi jobber med. Dette eksempelvis være delte servere, nettverk, byggesystemer, API, med mer.
- **Miljø:** En samling av alle ressursene som trengs for å kjøre applikasjonen vi jobber med. Det miljøet som brukerne av applikasjonen bruker, kalles typisk produksjon. Andre viktige miljøer er testmiljøer, som brukes for å sjekke at nye versjoner av applikasjonen fungerer før de installers i produksjonsmiljøet, og lokalt miljø, som brukes av utviklere for å sjekke at applikasjonen fungerer mens man utvikler.
- **Infrastruktur:** Samlingen av alle ressursene og miljøene en applikasjon bruker.
- **Infrastruktur som Kode:** En tilnærming hvor man automatiserer oppsett av infrastruktur ved å uttrykke den som kode.

_**Spørsmål:** Har du hørt noen andre rare CI/CD-ord du lurer på hva betyr? Er det forklaringer i ordlisten over du er uenig i?_

🎬 Litt om GitHub Actions
-------------------------
Actions er et verktøy for å bygge CI/CD-pipelines. Det er tilgjengelig direkte i GitHub, og settes opp ved at man legger inn spesielle YAML-filer i mappen `.github/workflows`.

Hver fil definerer en [workflow](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions#workflows). En workflow er en automatisk prosess som vi ønsker at skal kjøre når en spesiell hendelse skjer. Dette kan eksempelvis være at man bygger og tester koden automatisk når det kommer inn nye endringer i en pull-request, eller at man bygger, tester og installerer koden i et miljø når nye endringer kommer inn på main-branchen i repoet.

Hendelsene som starter en workflow kaller vi en [event](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions#events). Dette er ofte hendelser knyttet til koderepoet, f.eks. at ny kode kommer inn på en spesiell branch, eller at noe nytt skjer i en pull-request, men det kan også være at man manuelt kan lage en event, eller at man kan trigge eventer jevnlig, for f.eks. å oppdatere pakker som applikasjonen er avhengig av.

Hver workflow består av en eller flere [jobber](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions#jobs), som igjen kan bestå av ett eller flere steg. Eksempelvis kan man ha en jobb som bygger applikasjonen, som igjen innholder ett steg som installerer pakker som applikasjonen trenger, og ett steg som kjører en kommando for å bygge koden.

For å gjøre det lettere å lage worksflows som trenger å gjennomføre komplekse, men mye brukte oppgaver, kan man bruke [actions](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions#actions). Det finnes mange [ferdige actions](https://docs.github.com/en/actions/learn-github-actions/finding-and-customizing-actions) man kan bruke, og man kan [lage sine egne actions](https://docs.github.com/en/actions/creating-actions) hvis man trenger det.

Når en workflow skal kjøre, trenger den et sted å kjøre. Dette kaller man [runners](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions#runners). GitHub leverer noen ferdige runnere som man kan bruke, og det går også ann å sette opp sine egne.

### Hei på deg GitHub Actions!
Start med å forke repoet [github-actions-101](https://github.com/teodor-elstad/github-actions-101), sånn at du får en kopi av repoet knyttet til din egen GitHub-bruker. Deretter er det bare å klone din kopi av repoet lokalt på maskinen din.

Lag en fil som heter `hello-actions.yml` under mappen `.github/workflows`. Lim in koden under, og sjekk den inn. Gå til Actions-fanen i din klone av "github-actions-101"-repoet, og se hva som skjer her.

```yaml
name: "Hello Actions!"

on:
  workflow_dispatch:
  push:

jobs:
  say-hello:
    runs-on: ubuntu-latest
    steps:
      - name: "Echo Greeting"
        run: "echo 'Hei på deg GitHub Actions!'"
      - name: "Echo Goodnight"
        run: "echo 'Natta!'"
```

[YAML](https://en.wikipedia.org/wiki/YAML)-koden over, definerer en workflow med navnet "Hello Actions!". Den er satt opp til å trigge (`on:`) når en av to eventer skjer: Enten hvis workflowen blir startet manuelt fra grensesnittet til GitHub (`workflow_dispatch:`), eller hvis det pushes en commit til koderepoet (`push:`).

Når workflowen kjører, starter den en jobb som heter `say-hello`. Denne jobben kjører på en maskin som har en nyere versjon av operativsystemet [Ubuntu](https://ubuntu.com/) installert. Selve jobben består av to steg. Det første steget heter "Echo Greeting", og det bruker kommandoen [echo](https://linux.die.net/man/1/echo) til å printe teksten _Hei på deg GitHub Actions!_ til terminalen. Det andre steget ligner veldig på det første, men printer i stedet teksten _Natta!_.

_**Oppgave:** Klarer du å utvide workflowen over med ett steg til som kjører en annen valgfri kommando?_

_**Tips:** Når du er ferdig med denne oppgaven, kan det være lurt å fjerne `push:`-triggeren, så workflowen "Hello Actions!" ikke kjører hele tiden resten av kurset._

🏗️ Vi bygger og tester Notes.Api
--------------------------------
Nå skal vi ta i bruk GitHub Actions til å lage en enkel workflow som bygger og tester _Sticky Notes_-applikasjonen som finnes i dette repoet. En slik workflow er ofte et viktig steg i en større CI/CD-pipeline, og den kjøres gjerne før man merger inn ny kode i repoet, ofte som en del av en pull request prosess. Målet er å finne ut om applikasjonen fremdeles bygger og ser ut til å fungere som den skal.

### Hvordan tester man Notes.Api lokalt?
Siden CI/CD-pipelines i stor grad bare er en litt avansert skript som kjører en serie med terminal-kommandoer, og GitHub Actions langt på vei bare er et verktøy som gjør det lettere å skrive sånne skript, er det ofte lurt å starte prosessen med å utvikle en ny workflow lokalt i sin egen terminal. Har man god oversikt over hvilke kommandoer man trenger å kjøre lokalt, for å få til det man ønsker, blir det ofte mye lettere å utvikle selve workflowen etterpå. Vi starter derfor denne seksjonen av kurset, med å se på hvordan man kan bygge og teste _Sticky Notes_-applikasjonen lokalt.

_Sticky Notes_ er en applikasjon som er utviklet med [.NET](https://dotnet.microsoft.com/en-us/), så derfor kommer vi til å bruke terminal-programmet [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/) for å installere avhengigheter, bygge og teste applikasjonen.

#### Installasjon av avhengigheter
Man kan bruke kommandoen [`dotnet restore`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-restore) for å installere avhengigheter i .NET-applikasjoner. Disse avhengighetene, sammen med annen bygg-relevant informasjon, er satt opp i prosjektfiler, som har fil-endelsen `.csproj`. Vi kan kjøre kommandoen under for å restore alle avhengighetene i test-prosjektet `Notes.Api.Test`.

```shell
github-actions-101$> dotnet restore Notes.Api.Test/Notes.Api.Test.csproj
  Determining projects to restore...
  Restored /github-actions-101/Notes.Api/Notes.Api.csproj (in 220 ms).
  Restored /github-actions-101/Notes.Api.Test/Notes.Api.Test.csproj (in 257 ms).
```

Legg merke til hvordan vi også restoret avhengighetene i selve API-prosjektet `Notes.Api`. Dette skjedde fordi test-prosjektet `Notes.Api.Test` er avhengig av `Notes.Api`-prosjektet.

#### Bygging
For å bygge test-prosjektet bruker vi kommandoen [`dotnet build`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build). Her har vi også lyst til å bruke flagget `--configuration Release`, som forteller .NET CLI at vi har lyst til å bygge optimalisert for kjøring/release i et miljø, og ikke for debugging, som typisk er tilfellet når vi utvikler lokalt. I tillegg har vi lyst til å bruke flagget `--no-restore`, som forteller .NET CLI at vi ikke trenger å sjekke om det må installeres noen avhengigheter først, siden vi akkurat restoret prosjektet.

```shell
github-actions-101$> dotnet build --no-restore --configuration Release Notes.Api.Test/Notes.Api.Test.csproj
Microsoft (R) Build Engine version 17.0.1+b177f8fa7 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Notes.Api -> /home/teodoran/nrk/github-actions-101/Notes.Api/bin/Release/net6.0/Notes.Api.dll
  Notes.Api.Test -> /home/teodoran/nrk/github-actions-101/Notes.Api.Test/bin/Release/net6.0/Notes.Api.Test.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:02.37
```

Igjen ser vi at vi ved å bygge `Notes.Api.Test` også bygget `Notes.Api`, fordi test-prosjektet er avhengig av selve API-prosjektet.

#### Kjøring av testene
For å kjøre selve testene bruker vi kommandoen [`dotnet test`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test). Igjen skal vi bruke flagget `--configuration Release`, og i tillegg flagget `--no-build`, som forteller test-kommandoen at man ikke trenger å sjekke om test-prosjektet må bygges før testene kjører.

```shell
github-actions-101$> dotnet test --no-build --configuration Release Notes.Api.Test/Notes.Api.Test.csproj
Test run for /home/teodoran/nrk/github-actions-101/Notes.Api.Test/bin/Release/net6.0/Notes.Api.Test.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.0+68bd10d3aee862a9fbb0bac8b3d474bc323024f3
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     9, Skipped:     0, Total:     9, Duration: 265 ms - /home/teodoran/nrk/github-actions-101/Notes.Api.Test/bin/Release/net6.0/Notes.Api.Test.dll (net6.0)
```

Ni tester passerte! Neppe den mest omfattende test-suiten, men godt nok for det vi skal gjøre i dag.

#### Generering av testresultater
Kommandoen `dotnet test` kan kjøres med flere andre nyttige flagg. Eksempelvis kan flagget `--verbosity` brukes for å styre hvor mye informasjon testene skal printe til terminalen, og flagget `--logger` kan brukes for å få generert en fil som inneholder testresultatene (dette kan vise seg å være nyttig senere).

```shell
github-actions-101$> dotnet test \
                        --no-build \
                        --configuration Release \
                        --verbosity normal \
                        --logger trx \
                        Notes.Api.Test/Notes.Api.Test.csproj
Build started 10/18/2022 8:43:17 PM.
Test run for /github-actions-101/Notes.Api.Test/bin/Release/net6.0/Notes.Api.Test.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.0+68bd10d3aee862a9fbb0bac8b3d474bc323024f3
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
Results File: /github-actions-101/Notes.Api.Test/TestResults/_HAL_2022-10-18_20_43_20.trx

Passed!  - Failed:     0, Passed:     9, Skipped:     0, Total:     9, Duration: 268 ms - /home/teodoran/nrk/github-actions-101/Notes.Api.Test/bin/Release/net6.0/Notes.Api.Test.dll (net6.0)

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:02.21
```

Etter at du har kjørt kommandoen, kan du ta en titt på `.trx`-filen som ble generert i mappen `/github-actions-101/Notes.Api.Test/TestResults/`. Mye XML her, men kanskje det finnes noe som kan vise frem innholdet av filen på en fin måte?

_**Tips:** På noen plattformer kan man bryte ned lange kommandoer over flere linjer ved å bruke `\` på slutten av hver linje. Dette kan gjøre kommandoene lettere å lese. Hvis dette ikke fungerer i din terminal, kan du bare gjøre om kommandoen i eksempelet over til en lang linje._

### Restore, bygg og test i en workflow
Nå som vi har god oversikt over hva som skal til for å restore, bygge og teste en .NET-applikasjon, gjenstår det bare å skrive en GitHub Actions workflow som gjør det samme som vi akkurat har gjort i terminalen.

Du kan ta utgangspunkt i workflowen under, ved å legge den inn i en fil som f.eks. heter `hello-dotnet.yml` under mappen `.github/workflows`.

```yaml
name: "Hello .NET"

on:
  workflow_dispatch:

jobs:
  build-test:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v3

      - name: "Setup .NET Core SDK"
        uses: actions/setup-dotnet@v2
        with:
          dotnet-version: "6.0.x"

      - name: "Print .NET CLI version"
        run: dotnet --version
```

Denne workflowen inneholder allerede en jobb som har tre steg:
1. [actions/checkout](https://github.com/marketplace/actions/checkout) er en ferdig action, som sjekker ut koden vi har i repoet på maskinen jobben kjører på. Dette er en mye brukt action!
2. [actions/setup-dotnet](https://github.com/marketplace/actions/setup-net-core-sdk) installerer .NET CLI på maskinen, sånn at kommandoen `dotnet` er tilgjengelig for de neste stegene i jobben.
3. Til slutt kjører vi et steg som viser at vi har .NET CLI tilgjengelig ved å kjøre kommandoen `dotnet --version`.

_**Oppgave 1:** Legg til tre steg i workflowen over som restorer, bygger og kjører testene i `Notes.Api.Test`._

_**Oppgave 2:** Man kan bruke [miljøvariabler i GitHub Actions](https://docs.github.com/en/actions/learn-github-actions/environment-variables) til f.eks. å samle verdier som brukes flere steder i en workflow. Klarer du å lage en miljøvariabel med banen til test-prosjektet (`Notes.Api.Test/Notes.Api.Test.csproj`) som kan gjenbrukes i de forskjellige stegene?_

_**Oppgave 3:** `dorny/test-reporter` er en ferdig action som kan brukes til å vise frem testresultater. Klarer du å legge til et steg på slutten av jobben som bruker denne action til å vise frem `.trx`-testresultatene som `dotnet test` produserer?_

_**Tips:** Hvis du står fast på en av oppgavene over, er det bare å be om hjelp, eller ta en titt på eksempel-løsningen [her](https://github.com/teodoran/github-actions-101/blob/main/.github/workflows/hello-dotnet.yml)._

_**Info:** Man kan bruke [GitHub Actions Marketplace](https://github.com/marketplace) for å finne flere ferdige actions når du skal skrive dine egne workflows senere, men man trenger ikke flere for å løse oppgavene over._

🐋 Vi bygger og kjører en applikasjon med Docker
------------------------------------------------
Vi skal etter hvert lage en workflow som deployer `Notes.Api` med [Docker](https://www.docker.com/) og [Kubernetes](https://kubernetes.io/) (ofte bare kalt k8s). Det betyr at vi må se litt på hvordan man bruker disse verktøyene. Først ut er Docker (eller [Podman](https://podman.io/))!

_**NB:** Eksemplene under bruker kommandoen `docker`, så hvis du har valgt å installere `podman`, må du bruke `podman` i stede for `docker` når du kjører eksemplene._

### Hva er Docker?
Docker er en teknologi som lar oss pakke sammen programmer og en forenklet virtuell datamaskin til noe man kaller en container. Det er litt som at man i stede for å levere et dataprogram som må installeres på en annen maskin før man kan bruke det, leverer dataprogrammet ferdig installert på en datamaskin.

Container-teknologi, som Docker er et eksempel på, har flere fordeler:
- Man kan utvikle applikasjoner med mange forskjellige teknologier, men ved å putte de i en container, kan alle de forskjellige applikasjonene kjøres med samme teknologi, som bare trenger å vite hvordan man kjører en container.
- Man får større kontroll over datamaskinen applikasjonen kjører på i alle miljøer, siden denne datamaskinen i stor grad er pakket sammen med applikasjonen.
- Sammenlignet med tradisjonelle virtuelle datamaskiner (eller vanlige datamaskiner for den saks skyld), er containere mye enklere å overføre mellom forskjellige datamaskiner, og er lettere å bruke i en CI/CD-pipeline.

### Hva er et image?
For å lage en container, trenger man en mal. Denne malen kalles et image, og representerer et øyeblikksbilde av containeren etter at alle stegene i en gitt oppskriften er fulgt. Oppskriften kaller man gjerne en Dockerfile. En Dockerfile kan f.eks. inneholde stegene:
1. Start med et image Microsoft har laget, som inneholder operativsystemet Ubuntu med .NET CLI ferdig installert.
2. Kopier inn koden til applikasjonen vår, og bygg denne med .NET CLI.
3. Start den ferdig bygde applikasjonen vår.

Hvis man bygger et image fra denne oppskriften, vil man ende opp med et image som inneholder en spesifikk versjon av applikasjonen vår, basert på den koden som ble kopiert inn i imaget da oppskriften ble kjørt.

Sagt på en annen måte: Dockerfilen er en oppskrift som forteller oss hvordan man lager/byger et image. Hver kan man følger oppskriften, ender man opp med et image, som representerer en versjon av applikasjonen vår. Imaget kan man så senere dele og bruke til å lage containere.

### La oss leke med et image
Før vi skriver vårt eget image for applikasjonen `Notes.Api`, kan det være nyttig å leke litt med et ferdiglaget image. Et litt morsomt image å leke med, er [wernight/funbox](https://hub.docker.com/r/wernight/funbox). Dette imaget lager en container som inneholder et Linux operativsystem, hvor det er installert flere artige kommandoer som printer ut morsom tekst til terminalen. En slik kommando er [`sl`](https://manpages.ubuntu.com/manpages/bionic/man6/sl.6.html). For å kjøre denne kommandoen i containeren som bygges av wernight/funbox-imaget, kan vi kjøre følgende kommando:

```shell
$> docker run -it wernight/funbox:latest sl
```

Hva skjedde her?
- `docker run -it` forteller Docker at vi har lyst til å kjøre containeren vi lager fra imaget i "interactive mode", og at vi har lyst til å få vist frem det som printes til terminalen i containeren i vår egen terminal.
- `wernight/funbox:latest` er en referanse til et image. `wernight` er den som har publisert dette imaget, `funbox` er navnet på imaget og `latest` er versjonen. Denne referansen til et image kalles ofte en _image tag_ eller bare _tag_.
- Imaget vi lager containeren fra, forventer at vi sender med kommandoen vi vil kjøre i containeren som et argument. Derfor er det siste argumentet over `sl`, som er kommandoen vi ønsker å kjøre inne i containeren.

_**Oppgave:** Ta en titt på de andre kommandoene som finnes i wernight/funbox og prøv de ut! Hvis terminalen blir rotete, kan du tømme den med kommandoen `clear`._

### Publisering av Notes.Api med `dotnet publish`
Akkurat som når man skal skrive en workflow, er det nyttig å vite hvilke kommandoer man trenger å kjøre lokalt for å bygge og starte applikasjonen man skal skrive en Dockerfile for. Det å skrive en Dockerfile er nemlig ikke helt forskjellig fra å skrive skript det heller. Siden vi ønsker at containeren vår skal bygge og kjøre `Notes.Api`, starter vi med å ta en titt på hvordan man gjør dette lokalt med .NET CLI.

Når man skal bygge .NET-applikasjoner for leveranse og installasjon på andre maskiner, bruker man kommandoen [`dotnet publish`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish) i stede for `dotnet build`. I tillegg til blant annet å bygge applikasjonen, pakker denne kommandoen sammen alle filene den ferdig bygde applikasjonen består av, og putter de i en mappe, klare for å leveres og kjøres på en datamaskin.

```shell
github-actions-101$> dotnet publish --output ./Notes.Published --configuration Release --self-contained false Notes.Api/Notes.Api.csproj
Microsoft (R) Build Engine version 17.0.1+b177f8fa7 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  Notes.Api -> /home/teodoran/nrk/github-actions-101/Notes.Api/bin/Release/net6.0/Notes.Api.dll
  Notes.Api -> /home/teodoran/nrk/github-actions-101/Notes.Published/
```

Kommandoen over publiserer `Notes.Api` til en mappe som heter `Notes.Published`. Akkurat som når vi kjørte testene, bruker vi flagget `--configuration Release` for å fortelle `dotnet publish` at vi ønsker å bygge applikasjonen for leveranse.

_Vi har i tillegg med flagget `--self-contained false`, men å gå inn på hva dette betyr, er litt utenfor scope i denne omgangen. De som er spesielt interesserte kan ta en titt på [denne oversikten](https://learn.microsoft.com/en-us/dotnet/core/deploying/)._

Nå kan vi starte den ferdig publiserte applikasjonen direkte med `dotnet`. Naviger ned i mappen `Notes.Published` og kjør følgende kommando:

```shell
github-actions-101$> cd Notes.Published/
github-actions-101/Notes.Published$> dotnet Notes.Api.dll
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/teodoran/nrk/github-actions-101/Notes.Published/
info: Microsoft.Hosting.Lifetime[0]
```

Hvis alt gikk bra, kan du åpne [http://localhost:5000/client](http://localhost:5000/client) og leke litt med _Sticky Notes_-applikasjonen. Du kan også pinge APIet direkte ved å gå til [http://localhost:5000/ping](http://localhost:5000/ping).



### Logg inn med docker
Før vi kan pushe docker-imager til registeret `devops101registry.azurecr.io`, må vi logge inn med `docker`. Kjør kommandoen under, og logg på med [dette brukernavnet](https://nrkconfluence.atlassian.net/wiki/spaces/PTU/pages/106109005/GitHub+Actions+101+kurs+h+st+2022#CONTAINER_REGISTRY_USERNAME) og [dette passordet](https://nrkconfluence.atlassian.net/wiki/spaces/PTU/pages/106109005/GitHub+Actions+101+kurs+h+st+2022#CONTAINER_REGISTRY_PASSWORD).

```shell
$> docker login devops101registry.azurecr.io
Username: devops101registry
Password:

WARNING! Your password will be stored unencrypted ...

Login Succeeded
```

Hvis alt gikk som det skulle, skal den siste meldingen fra kommandoen være "Login Succeeded".

_Får du en advarsel om at passordet kommer til å lagres ukryptert, er det bare å se bort ifra denne._


Punkter:
- Gjennomgang av hvordan man bygger, kjører og bruker Notes.Api lokalt.
- Intro til Docker, hvor man leker med ferdige kommandoer.
- Guide til å lage en enkel Dockerfil for Notes.Api.
- Oppgave: Kan du gjøre bygget kjappere ved å cache `dotnet restore`-steget? (Husk fasit)
- Oppgave: Kan du gjøre imaget mindere ved å sette opp en `.dockerignore`-fil? (Husk fasit og info om hvordan man ser hvor stort imaget er)
- Hvordan deler man imager med omverdenen?

_Husk at man må legge opp til at deltakerne prefikser image med brukernavn f.eks. tae-notes-api._

⎈ Vi deployer en applikasjon til Kubernetes
--------------------------------------------

### Konfigurer kubectl med tilgang til devops-101-cluster
For å kunne jobbe med Kubernetes-klyngen `devops-101-cluster`, må vi konfigurere `kubectl`. Konfigurasjonen vi trenger finner du [her](https://nrkconfluence.atlassian.net/wiki/spaces/PTU/pages/106109005/GitHub+Actions+101+kurs+h+st+2022#KUBERNETES_CLUSTER_CONFIG).

Det er flere måter man kan få `kubectl` til å bruke denne konfigurasjonen på:
1. Man kan lagre konfigurasjonen i en egen fil, og bruke argumentet `--kubeconfig` til å fortelle `kubectl` at man skal bruke denne konfigurasjonen: `kubectl get pods --kubeconfig ~/devops-101-config.yaml`. Hvis man velger denne løsningen, må man huske å legge på argumentet `--kubeconfig` på alle `kubectl`-kommandoer man kjører videre i kurset.
2. Man kan flette inn elementene fra `clusters`, `contexts` og `users` i `kubectl` sin default konfigurasjonsfil (ofte kalt kubeconfig-filen), som er `~/.kube/config` på Linux og Mac, og `%USERPROFILE%\.kube\config` på Windows. Flettingen gjør man ved å åpne kubeconfig-filen i en teksteditor, og klippe inn de forskjellige delene der de hører hjemme. I tillegg må man oppdatere `current-context` til `devops-101-cluster`.
3. Man kan lagre konfigurasjonen i en egen fil, og så [opprette miljøvariabelen](https://kubernetes.io/docs/tasks/access-application-cluster/configure-access-multiple-clusters/#set-the-kubeconfig-environment-variable) `KUBECONFIG`, med en bane til denne (og flere) konfigurasjonsfiler. Dette er ofte den mest praktiske løsningen hvis man jobber mye med forskjellige konfigurasjoner, men kan være den mest omstendelige løsningen å sette opp.

Velg en av metodene over for å konfigurere `kubectl` til å nå `devops-101-cluster`. Når du har gjort dette, skal du kunne hente alle poddene som kjører i navnerommet `kube-system`, og få et svar litt som vist under:
```shell
$> kubectl get pods --namespace kube-system
NAME                                  READY   STATUS    RESTARTS   AGE
azure-ip-masq-agent-9g24h             1/1     Running   0          14h
cloud-node-manager-ztb2l              1/1     Running   0          14h
coredns-autoscaler-5589fb5654-sknvl   1/1     Running   0          14h
coredns-b4854dd98-56fgr               1/1     Running   0          14h
coredns-b4854dd98-fzf48               1/1     Running   0          14h
csi-azuredisk-node-8cshv              3/3     Running   0          14h
csi-azurefile-node-kxl6c              3/3     Running   0          14h
konnectivity-agent-cb784597d-b55lq    1/1     Running   0          14h
konnectivity-agent-cb784597d-wxwql    1/1     Running   0          14h
kube-proxy-n45m4                      1/1     Running   0          14h
metrics-server-f77b4cd8-crwl9         1/1     Running   0          14h
metrics-server-f77b4cd8-jq29q         1/1     Running   0          14h
```

Punkter:
- Kort intro til Kubernetes.
- Gjennomgang av ferdig oppsatt deployment og service.
- Guide til å deploye Notes.Api manuelt.
- Oppgave: Skalere Notes.Api manuelt?

_Husk at man må legge opp til at deltakerne prefikser k8s-ressurser med brukernavn f.eks. tae-notes-api._

🚀 Vi bygger en workflow som deployer Notes.Api
-----------------------------------------------

![](Images/fra-docker-til-kubernetes.png)

Punkter:
- Kort recap av hva som må gjøres for å deploye applikasjonen:
  1. Bygge Docker-image med ny image-tag.
  2. Pushe Docker-image til registry.
  3. Oppdatere Kubernetes-config til å bruke ny image-tag.
  4. Kjøre oppdatert Kubernetes-config inn i klyngen.
- Guide til å sette opp pipeline.
- Har vi en oppgave her?
- Oppgave: Sett opp triggere sånn at test-pipelinen kjører på PR'er og deploy-pipelinen kjører når ny versjon kommer inn på main.
- Spørsmål: Hvilke feil fanges opp av dette oppsettet? Hvilke feil fanges ikke opp?



🔗 Vi knytter sammen workflows til en CI/CD-pipeline
----------------------------------------------------

![](Images/masse-fra-utvikler-til-produksjon.png)
