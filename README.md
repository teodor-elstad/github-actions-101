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

Når vi kommer så langt i kurset at man skal begynne å installere applikasjonen i forskjellige miljøer, trenger du [Docker](https://docs.docker.com/get-docker/) eller [Podman](https://podman.io/getting-started/installation) for å bygge kontainere, og [kubectl](https://kubernetes.io/docs/tasks/tools/#kubectl) for å orkestrere kontainerene du bygger i Kubernetes.

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

_**Oppgave:** Klarer du å utvide workflowen over med ett steg til som kjører en valgfri kommando?_

🏗️ Vi bygger en CI/CD-pipeline!
-------------------------------
Nå skal vi ta ibruk GitHub Actions til å lage en enkel CI/CD-pipeline som bygger, tester og installerer _Sticky Notes_ applikasjonen som finnes i dette repoet.

![](Images/masse-fra-utvikler-til-produksjon.png)

Punkter:
- Gjennomgang av hvordan man bygger å kjører Notes.Api.Test lokalt.
- Intro til marketplace, og Actions man trenger for å lage en bygg-pipeline.
- Guide til å sette opp starten på en pipeline.
- Oppgave: Gjør ferdig pipelinen sånn at man kjører Notes.Api.Test (Husk fasit)
- Oppgave: Legg til en action som rapporterer testresultatene (Husk fasit)

🐋 Vi bygger og kjører en applikasjon med Docker
------------------------------------------------

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

Punkter:
- Kort intro til Kubernetes.
- Gjennomgang av ferdig oppsatt deployment og service.
- Guide til å deploye Notes.Api manuelt.
- Oppgave: Skalere Notes.Api manuelt?

_Husk at man må legge opp til at deltakerne prefikser k8s-ressurser med brukernavn f.eks. tae-notes-api._

🚀 Vi bygger en workflow som deployer Notes.Api
-----------------------------------------------

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

🔐 Vi tester ut konfigurasjon av docker og kubectl
--------------------------------------------------

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