# ProjectSystemsSmoke (.NET 11)

A test repository for smoke-testing **Rider Project Systems** (Solution view, Repo view, toolset, NuGet, run/test)
on **.NET 11 / C# 15**, with .NET 10, .NET Framework 4.8, F#, Blazor, Aspire and a standalone frontend mixed in.


## Requirements

| What | Needed for |
| --- | --- |
| .NET SDK **11.0.100-rc.1+** (pinned in `global.json`, `rollForward: latestFeature`) | everything |
| .NET SDK **10.0.3xx** | `sdk10/` (pinned by `sdk10/global.json`) |
| Windows + Visual Studio / Build Tools MSBuild | *building* `legacy/Smoke.NetFx.Legacy` (old-style csproj, packages.config) |
| Windows | *running* `Smoke.Wpf`, `Smoke.WinForms` (they build on every OS: `EnableWindowsTargeting`) |
| Node.js 20+ | `frontend/vite-react`, `src/Smoke.Esproj`, `src/Smoke.Web/ClientApp` (Angular), TypeScript in `Smoke.Rcl` (`scripts/setup-frontend.ps1`) |
| Internet | NuGet restore (incl. `Microsoft.Build.Traversal`, `Aspire.AppHost.Sdk`, `Microsoft.VisualStudio.JavaScript.Sdk`) |

> Items marked **(RC2+)** are documented for .NET 11 RC 2 and don't work on RC 1 yet. They are kept on purpose so they start working after an SDK update.

## Repository layout

```
ProjectSystemsSmoke.slnx          XML solution: solution folders/items, configs Debug/Release/Smoke x Any CPU/x64, Build=false for Broken + Legacy
ProjectSystemsSmoke.sln           Classic-format twin of the .slnx (same projects, same folders) - compare both
ProjectSystemsSmoke.Core.slnf     Solution filter made with the new `dotnet sln <slnf> add` (.NET 11)
global.json                       SDK pin, MTP test runner, msbuild-sdks (Traversal)
Directory.Build.props/.targets    Implicit imports for every project (+ custom target that prints a line per build)
Directory.Packages.props          Central Package Management
nuget.config                      nuget.org + local folder feed `packages-local/`
dirs.proj                         Microsoft.Build.Traversal project (`dotnet test dirs.proj` is new in .NET 11)
.config/dotnet-tools.json         Local tool manifest (dotnetsay)
.gitattributes, .editorconfig     Cross-platform line endings / code style

src/
  Smoke.Library/        net11.0;net10.0;netstandard2.1;net48. C# 15 code under #if NET11_0_OR_GREATER
  Smoke.Console/        net11.0 app: launch profiles, env vars, OS-conditional Compile items, RuntimeIdentifiers,
                        source generator (Analyzer ProjectReference), shared project import
  Smoke.CSharpPreview/  LangVersion=preview + AllowUnsafeBlocks: C# 15 memory-safety preview
  Smoke.FSharp/         F# library (compile order) referencing the C# library
  Smoke.Generators/     Incremental source generator (netstandard2.0, IsRoslynComponent)
  Smoke.Shared/         Shared project (.shproj/.projitems) imported by Console and Tests
  Smoke.Web/            ASP.NET Core Web API + wwwroot (HTML/CSS/JS) + .http file
  Smoke.Blazor/         Blazor Web App (Server interactivity), uses Smoke.Rcl and Smoke.Library
  Smoke.Rcl/            Razor Class Library + TypeScript (Microsoft.TypeScript.MSBuild, opt-in)
  Smoke.AppHost/        Aspire AppHost (Aspire.AppHost.Sdk 13.6) orchestrating Web + Blazor
  Smoke.Esproj/         JavaScript project *inside* the solution (Microsoft.VisualStudio.JavaScript.Sdk)
  Smoke.Wpf/            net11.0-windows WPF
  Smoke.WinForms/       net11.0-windows WinForms
  Smoke.NetFx.Sdk/      SDK-style net48 with PackageReference (builds on any OS)
  Smoke.VisualBasic/    VB project with Actions/Copy + Actions/Rename (vb, css, html, js, min.js)
  Smoke.ProjectModel/   Solution Explorer playground (see table below)
  Smoke.StopIndex/      Stop Index fixture: folders in/out of solution x in/out of index (state in .idea/.../indexLayout.xml)
  Smoke.Navigation/     Search Everywhere / Find in Files fixture (FilesToSearch, file masks, removed files, _csharp)
  Smoke.Broken/         Intentionally broken project (missing import + missing project reference)
  Smoke.Web/ClientApp/  Angular app INSIDE the web project (SpaRoot pattern) with src/Rename mirror of the Actions files
legacy/
  Legacy.sln            Classic solution with the old-style projects only
  Smoke.NetFx.Legacy/   Non-SDK net48 csproj, packages.config, HintPath reference
  Smoke.Wpf.Legacy/     Non-SDK net48 WPF (ProjectTypeGuids, App.xaml, Resources.resx, App.config)
solutions/
  ProjectSystemsSmoke.Web.slnx   Solution in a subfolder: all project paths point outside (../src/...)
.idea/.idea.ProjectSystemsSmoke/.idea/
  indexLayout.xml       TRACKED: attached folders, explicit index includes/excludes used by StopIndex/Navigation
  scopes/*.xml          TRACKED: "Main Actions Scope", "Blazor Scope", "Attached Folder Scope"
sdk10/
  global.json           Pins the .NET 10 SDK
  Sdk10.slnx            Separate solution with a net10.0 app
tests/Smoke.Tests/      xUnit v3 on Microsoft.Testing.Platform
file-based/             File-based apps (#:include, #:project, #:property, #:package)
frontend/vite-react/    Standalone Vite + React + TypeScript app, NOT in any solution (Repo view)
file.txt, package-lock.json   Root-level traps: empty file; a lock file without package.json (JS project detection)
shared/                 Files linked into Smoke.ProjectModel
vendor/nested-repo/     A second git repository nested in the main one
docs/, db/, scripts/, notes/, .github/, Dockerfile, .env, CODEOWNERS, CHANGELOG.md   Non-solution repo files
```

### C# 15 features

| Feature | Where |
| --- | --- |
| Union types (`union Pet(Cat, Dog, Bird)`) | `src/Smoke.Library/Unions/Pet.cs` |
| Closed hierarchies (`closed record`) | `src/Smoke.Library/Unions/GateState.cs` |
| Extension indexers | `src/Smoke.Library/Extensions/SequenceIndexer.cs` |
| Labeled `break` / `continue` | `src/Smoke.Library/Extensions/Grid.cs` |
| Collection expression arguments (`[with(...), ..]`) | `src/Smoke.Library/Extensions/Grid.cs` |
| Memory safety preview: pointers/`fixed` outside `unsafe`, `unsafe(expr)` | `src/Smoke.CSharpPreview/Program.cs` |

### Smoke.ProjectModel playground

| Folder / item | What it is for |
| --- | --- |
| `Actions/Copy/`, `Actions/Move/`, `Actions/Rename/`, `Actions/Delete/` | Copy/paste, move (namespace update), rename (usages), delete + Undo/Redo. Each folder has `.cs`, `.html`, `.js` + `.min.js` (nested), `.css`, `.json` and a `.ts` in a subfolder |
| `Actions/Test.cs`, `Test.cs`, `Test.ts` (unresolved import), `mydir/nested/file.txt` | Same-named files at different levels; TS error; deeply nested non-code file |
| `.gitignore` in the project + `Actions/Copy/CopyMe.local.json` | Project-level ignore file (.ignore plugin, VCS colors) |
| `src/Smoke.Web/Actions/{Copy,Rename}` | Web flavour: `.cshtml` + `.cshtml.cs`, `.razor` + `.css` (`DependentUpon`), `AdditionalFiles`, `.html/.js/.min.js/.ts` |
| `src/Smoke.Web/ClientApp/src/Rename` | Same files inside the SPA folder (`None` items): copy/move between a .NET folder and `ClientApp` |
| `Excluded/ExcludedFile.cs`, `Excluded/ExcludedFolder/` | `<Compile Remove>`: doesn't compile if included back |
| `Nested/Report.cs` ← `Report.Designer.cs`, `settings.json` ← `settings.Development.json` | `DependentUpon` nesting |
| `Resources/Strings.resx` ← `Strings.Designer.cs`, `Strings.de.resx` | ResX + designer (ResXFileCodeGenerator) + satellite culture |
| `Templates/Hello.tt` ← `Hello.cs` | T4 template with `LastGenOutput` |
| `Linked/SharedCode.cs`, `Linked/Assets/*` | `Link` / `LinkBase` items from `/shared` |
| `Links/` (created by `scripts/create-links.*`) | Symbolic links to a file and a folder, one hard link (git-ignored) |
| `Wildcards/**/*.txt`, `Wildcards/**/*.skip.cs` | Wildcard include / remove |
| `CaseSensitivity/data.json` referenced as `DATA.JSON` | Case-sensitivity: fine on Windows/macOS, mismatch on Linux |
| `Special Chars/Папка/ünïcödé-файл.cs`, `file with spaces.cs`, `#hash & (parens) + plus.txt` | Spaces, non-ASCII and URL-special characters in paths |
| `Content/data.json`, `Content/custom.smoke` | `CopyToOutputDirectory`; custom item type `SmokeItem` from `Imports/ProjectModel.props` |
| `Imports/ProjectModel.props` | Explicit `<Import>` → *Show Project Imports* |

## How to test

Start with a **cold start**: fresh clone, or delete `.idea`, `bin`, `obj`. Use [RIDER-A-1660](https://youtrack.jetbrains.com/articles/RIDER-A-1660) for anything not specific to this repo.

### 0. Command-line baseline (outside Rider)

```bash
dotnet build ProjectSystemsSmoke.slnx
```

```bash
dotnet test --solution ProjectSystemsSmoke.slnx
```

```bash
dotnet build ProjectSystemsSmoke.slnx -c Smoke -p:Platform=x64
```

EXPECTED: build succeeds (every project prints `[ProjectSystemsSmoke] <Project> -> <TFM> built with SDK 11.0...`), 8 tests pass.
`Smoke.Broken` and `Smoke.NetFx.Legacy` are skipped (`Build="false"` in the solution). Rider should behave the same as the CLI.

### 1. Solution opening

* Open the **folder** → Rider must offer `ProjectSystemsSmoke.slnx`, `ProjectSystemsSmoke.sln` and `legacy/Legacy.sln`, `sdk10/Sdk10.slnx`.
* Open `ProjectSystemsSmoke.slnx` and `ProjectSystemsSmoke.sln` → same tree: folders `Solution Items` (+ `images`), `Empty`, `file-based`, `src/{Web,Desktop,NetFramework,ProjectModel,Navigation}`, `tests`, 22 projects.
  `Smoke.Broken` shows a load error (missing `DoesNotExist.props`); nothing else is red.
* Open `solutions/ProjectSystemsSmoke.Web.slnx` → 4 projects whose paths are all outside the solution folder; Solution Items from `../`.
* Open `ProjectSystemsSmoke.Core.slnf` → only Console, Generators, Library, Shared, Tests are loaded.
* Open `sdk10/Sdk10.slnx` in a second window → SDK 10.0.3xx is used (Toolset settings, build output).
* Open `legacy/Legacy.sln` (Windows) → Visual Studio MSBuild is detected; packages.config restore makes `Newtonsoft` resolve; `Smoke.Wpf.Legacy` shows XAML designer files nested (`App.xaml.cs`, `Resources.Designer.cs`).
* Welcome screen, File | Open, recent list, command line, Toolbox, drag & drop, Open in New Window + close quickly, cancel loading, reopen on startup.
* Clone to an untrusted location → MSBuild security notification; trusted → none.

### 2. Toolset / SDKs / MSBuild

* Settings | Build, Execution, Deployment | Toolset and Build: .NET 11 RC SDK + MSBuild from it; on Windows the net48 targets should make Rider pick the VS MSBuild (compare auto-detect vs explicit).
* Rename the pinned SDK in `global.json` → Rider offers to install it (Environment wizard); revert.
* Solution context menu → *Manage .NET SDK for Solution*.
* Configuration selector: `Debug|Release|Smoke` × `Any CPU|x64`. Switch and build.
* `dotnet tool run dotnetsay hi` (local tool manifest) from the Rider terminal.

### 3. Solution Explorer / File System / Repo view

Use `Smoke.ProjectModel` (table above) for file actions, include/exclude, nesting, links, wildcards, case-sensitivity, special characters, resx, T4.

**Main actions** (repeat for `Smoke.ProjectModel`, `Smoke.Web` incl. `ClientApp/src/Rename`, `Smoke.FSharp`, `Smoke.VisualBasic`):

* Rename: projects; `Actions/Rename` folder and every file in it (`.cs`, `.html`, `.js`/`.min.js`, `.ts`, `.cshtml`, `.razor`) via *Refactor This*, context menu *Edit | Rename*, shortcut. `RenameUsage.cs` must follow.
* Copy + paste `Actions/Copy`: into the same location, another folder of the same project, `ClientApp/src`, another project (C# → F#/VB too), another solution opened in a second window.
* Move `Actions/Move` and single files from it: between folders, into `ClientApp/src`, to another project, to another solution. Namespaces are adjusted for `.cs`, not for `.js/.ts`.
* Delete `Actions/Delete` and `ClientApp/src/Rename` → Undo restores files and project items.
* Add on project level: file, directory, scratch, existing item, reference; on solution level: new project, existing project (`legacy/Smoke.Wpf.Legacy`, `sdk10/Smoke.Net10`).
* Include/exclude `Actions/Rename` and single files in it (nested `.min.js` and `.cshtml.cs` follow their parent). RIDER-63566 for XAML projects in solution folders (`src/Desktop`).
* `.min.js` is nested under `.js` by Rider's default nesting rules; `*.razor.css` by `DependentUpon`. Check *File Nesting Settings*.

**Stop index** (`Smoke.StopIndex`; state is in the tracked `indexLayout.xml`):

| Folder | In solution | Indexed |
| --- | --- | --- |
| `ExcludedFolder` | no | no |
| `ExcludedFromSolutionButInIndex` | no | **yes** (explicit include) |
| `FolderInIndex` | yes | yes |
| `FolderOutOfIndex` | yes | **no** (explicit exclude), shown as *no index* |
| `GeneratorOutput` | yes (empty `<Folder>` item) | no → write a file into it, it must stay unindexed |

* Search `TestName` / `Cat` / `Dog` in Search Everywhere and Find in Files: only indexed folders are found; non-indexed ones only with *Include non-solution items*.
* *Stop Index* on `StopIndexingMe.cs`, on `FolderInIndex`, on the whole project; *Start Index* back. `indexLayout.xml` changes accordingly (VCS diff).
* Same for `Smoke.Navigation/FolderOutOfIndex` and `tests/Smoke.Tests/FilesToSearch` (excluded in a test project).
* Attached folder `docs/` (from `indexLayout.xml`): attach/detach, *Attached Folder Scope* color, RIDER-61055, RIDER-60581, RIDER-61116.

**Properties / Copy Path / Locate**

* File/folder properties for one item and for a multi-selection (file + folder, RIDER-119389); *Build action* list; project properties (RIDER-60325).
* *Copy Path* from the editor tab, from Explorer nodes (file, folder, project, solution folder, solution) and from the Edit menu: Absolute, File Name, From Content Root, From Repository Root (different for `solutions/ProjectSystemsSmoke.Web.slnx`!), Path With Line Numbers, GitHub URL, Toolbox URL.
* *Locate/Select in Solution View*, *Select In*, *Always Select Opened File* for `.cs`, `.fs`, `.vb`, `.cshtml`, `.razor`, `.js`, `.ts`, a console/scratch, `mydir/nested/file.txt`, a file in `node_modules`.
* Restart with expanded solution folders, projects and folders → state is restored (RIDER-5094).

* Run `scripts/create-links.ps1` (or `.sh`) and check `Links/`: symlink file, symlink folder, hard link. Edit through the link → physical file changes. Delete the link → target stays.
* *Show Project Imports*: `Directory.Build.props/.targets`, `Directory.Packages.props`, `Imports/ProjectModel.props`, `Smoke.Shared.projitems`.
* Shared project `Smoke.Shared`: node in the tree, `SharedConstants` has usages in Console and Tests, edits are seen by both.
* Source generator: `Smoke.Console` → Dependencies | Analyzers | Smoke.Generators → `HelloGenerated.g.cs`; Go to Declaration on `HelloGenerated.Message`; also under `obj/.../generated`.
* `Smoke.Esproj`: JavaScript project node inside the solution; `npm` scripts; compare with `frontend/vite-react` outside the solution.
* Unload/Reload (`Smoke.Broken`: Edit project file, fix the import, Reload), Add existing project (`sdk10/Smoke.Net10`), attach folder (`docs/`).
* Build / Clean: `bin/Debug/net11.0/Content/data.json` and `CaseSensitivity/DATA.JSON` appear / disappear.
* Restart Rider → tree state is restored.

**Repo view (Files view)**

* The root must be the main repository, not `vendor/nested-repo` (RIDER-135532). The nested repo shows as a folder with its own `.git`.
* Non-solution files are visible: `.env`, `Dockerfile`, `CODEOWNERS`, `docs/`, `db/schema.sql`, `scripts/`, `.github/workflows/ci.yml`, `notes/`.
* `frontend/vite-react` is discovered as a web project: *Index Web Project* banner / context menu; Start/Stop index (RIDER-141802, RIDER-141817, RIDER-141818).
* After `scripts/setup-frontend.ps1`: `node_modules` indexing (top-level packages only), completion in `src/App.tsx`, Vitest run from `src/greeting.test.ts`.
* After `scripts/generate-ignored-files.ps1 -Count 5000`: git-ignored `local-cache/` is shown but not indexed; fast file search still finds `file-4999.json` (RIDER-137912); exclusion from indexing.
* Search Everywhere: `not-in-solution.txt`, `architecture.md`, `App.tsx`, `VendorLib.cs` (RIDER-141745, RIDER-141889).
* Scope colors: solution files vs non-solution (yellow) in Explorer, tabs, Search Everywhere, Find in Files.
* `src/Smoke.Web/ClientApp` is a second JS project *inside* a .NET project: both it and `frontend/vite-react` must be detected; Rider may run `npm install` for them on its own.

**Navigation fixtures** (`Smoke.Navigation`, `Smoke.StopIndex/FilesToSearch`, `Smoke.Web/FilesToSearch`, `Smoke.Tests/FilesToSearch`, `Smoke.Blazor/FileToSearch.*`, `Smoke.FSharp/FileToSearch.fs`)

* Search Everywhere (Go to All / File / Text): `FileToSearch` (same name in 4 projects and 6 file types), `File ToSearchFor` (space in the name), `_csharp/file.cs` (underscore folder, lower-case file vs `File` class), `R` / `t.html` in Blazor (one-letter names), `UAUAUA1`. RIDER-59079, RIDER-61397, RIDER-63634.
* Find in Files: `FooBar` → C#, JS, Razor, F# hits; `TestFileMaskWork/CSharpClass2.cs` and `JSscript2js.js` only with *Include non-solution items*; file masks `*.cs`, `*.js`, `!*.min.js`; *Except comments* drops the `//     FooBar` line; scopes Solution/Project/Directory/custom (`Main Actions Scope`). RIDER-43469, RIDER-59474, RIDER-20764, RIDER-59693, RIDER-64739.
* Speed search in Explorer: `rename me`, `renameme`, `RM` (CamelHumps), digits (`TestName1`), one character, ~50 characters, 255+ characters.
* Navigation bar on: no full paths for decompiled sources, scratches, consoles, source cache (RIDER-65773; also RIDER-59087, RIDER-64425, RIDER-59030). Navigation bar off: *Jump to Navigation Bar* reaches solution, projects, folders, files (`.cs`, `.fs`, `.vb`, `.cshtml`, `.razor`, `.js`, `.ts`, nested files), `node_modules`.
* Scopes: three predefined in `.idea/.../scopes` (`Main Actions Scope`, `Blazor Scope`, `Attached Folder Scope`) → file colors in Explorer/tabs, search in scope, add a new custom scope and check it is written next to them.

**Web / Angular** (`src/Smoke.Web/ClientApp`, needs Node)

* `node_modules`: only packages listed in `package.json` are indexed (RIDER-58303, RIDER-59127); search for `@angular/core` in Search Everywhere and Find in Files.
* `npx ng generate component test` in `ClientApp` → new files appear in Explorer without refresh (RIDER-61533); `npm start` → `https://localhost:5002`.
* Custom JS library: Settings | Languages & Frameworks | JavaScript | Libraries → add `@types/1line-aa`, restart → still listed (RIDER-62506); Find in Files for `1line-aa` (RIDER-63026, `docs/images/find-in-files.png`).
* Root `package-lock.json` without `package.json` and `file.txt` must not break JS project detection.

### 4. Multi-targeting, C# 15, cross-language

* `src/Smoke.Library/Unions/Pet.cs`: switch TFM in the editor context selector. `net11.0` → active C# 15 code; `net10.0`, `netstandard2.1`, `net48` → inactive (`#if`). `TargetInfo.Framework` differs per target (`net48` branch too).
* `Smoke.NetFx.Sdk` and `Smoke.NetFx.Legacy` reference `Greeter` from the **net48** build.
* `Smoke.CSharpPreview`: `int* pointer = &number;` and `fixed` outside `unsafe` have no errors (LangVersion=preview); turn `AllowUnsafeBlocks` off → errors.
* Navigation: Console/Tests → Library; F# `Library.fs` → C#; Blazor `Home.razor` → `Component1` in `Smoke.Rcl`; AppHost `Projects.Smoke_Web` → generated code.
* Assembly Explorer: open `Smoke.Library.dll` for each of the 4 TFMs.

### 5. Cross-platform (run once on Linux or macOS)

* `dotnet build ProjectSystemsSmoke.slnx` succeeds (WPF/WinForms build via `EnableWindowsTargeting`; Legacy is skipped).
* `Smoke.Console`: `Platform/PlatformInfo.Unix.cs` is compiled, `PlatformInfo.Windows.cs` is shown as a non-compiled item (reverse on Windows). Output: `Platform file: Unix`.
* Linux (case-sensitive FS): `CaseSensitivity\DATA.JSON` doesn't match `data.json` → Rider shows the item as missing / warns; build warns on copy.
* Special characters in `Special Chars/Папка/...` work in the tree, editor, Search Everywhere, VCS.
* `.gitattributes` normalizes line endings: no spurious VCS changes after opening on another OS.
* Symlinks via `scripts/create-links.sh`.

### 6. Run / Debug

* `Smoke.Console` profile **Smoke.Console** → `SMOKE_PROFILE = default`; profile **MSBuildArgs** **(RC2+)** → `Args: --name Smoke.Console --tfm net11.0` (RC 1 prints `$(AssemblyName)`).
* New `dotnet run -e`: `dotnet run --project src/Smoke.Console -e SMOKE_ENV=from-cli` → `SMOKE_ENV = from-cli`; same via run configuration env vars.
* `Smoke.Web` **http** → `http://localhost:5131/` loads `/hello/Rider`; run requests from `Smoke.Web.http`.
* `Smoke.Blazor` → Home page shows `Hello, Blazor! (net11.0)` and the RCL component.
* `Smoke.AppHost` → Aspire dashboard with `web` and `blazor` resources; `dotnet watch --project src/Smoke.AppHost` (new Aspire integration).
* `Smoke.Wpf`, `Smoke.WinForms` (Windows) → window title/text from `Greeter`; `legacy/Smoke.Wpf.Legacy` (from `Legacy.sln`) → text from `Resources.resx`.
* `Smoke.VisualBasic` → `Hello, VB! (net11.0)` + `RenameMe`; `Smoke.StopIndex` → `Tom`; `Smoke.Navigation` → `FooBar`.
* `frontend/vite-react`: `npm run dev` → `http://localhost:5173` proxies `/hello` to Smoke.Web.
* Breakpoints: `Program.cs`, `Smoke.Library` net11.0 code, Razor component, F#.

### 7. Unit tests

* Unit Tests window: 8 tests in `Smoke.Tests` (`CSharp15Tests` incl. 2 theory cases, `ProjectModelTests`). Run/Debug from gutter, Explorer, tool window.
* Vitest tests in `frontend/vite-react` and `src/Smoke.Esproj` after `npm install`.
* CLI parity (new .NET 11 `dotnet test` options):

  ```bash
  dotnet test --solution ProjectSystemsSmoke.slnx --results-directory-layout per-module
  ```

  ```bash
  dotnet test dirs.proj
  ```

### 8. File-based apps (`file-based/`)

* `app.cs`: `#:include helpers.cs`, `#:include models/customer.cs`, `#:project ../src/Smoke.Library/...` → no red code, navigation works, run from gutter and CLI:

  ```bash
  dotnet run --file file-based/app.cs
  ```

  (from the repo root use `--file`; inside `file-based/` just `dotnet app.cs`).
* `directives-rc2.cs` **(RC2+)**: quoted `#:property` values + `#:package ... Aliases=CommandLine`. RC 1: `Directives currently cannot contain double quotes`.
* `file-based/Directory.Packages.props` turns CPM off for that folder only.

### 9. NuGet

* NuGet window lists packages for Web, Blazor, Rcl, Tests, Generators, NetFx.Sdk, AppHost; versions come from `Directory.Packages.props` (update → props changes, not csproj).
* `nuget.config`: sources `nuget.org` + `local` (`packages-local/`). Drop a `.nupkg` there and install it.
* `legacy/Smoke.NetFx.Legacy`: packages.config restore (NuGet window / *Restore*), `HintPath` resolves after restore.
* `Smoke.Rcl` TypeScript: `dotnet build src/Smoke.Rcl -p:SmokeEnableTypeScript=true` (needs Node) → `wwwroot/js/greet.js` (new static-web-assets integration).
* `global.json` `msbuild-sdks`: delete `~/.nuget/packages/microsoft.build.traversal` and restore.

### 10. Investigation

* Tools | Internal Actions (Rider) | Dump | Project Model / IDEA Project Model ([internal mode](https://rider-support.jetbrains.com/hc/en-us/articles/207327910-How-to-run-JetBrains-Rider-in-Internal-Mode)).
* Help | Collect Logs and Diagnostic Data.

## Not covered here

MAUI (workload-dependent), Unity, Unreal, Wix, SQL projects, Xamarin, Mono, UWP, web references (ASMX/WSDL), paths longer than 260 characters.
