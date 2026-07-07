# FigmaJsonRenderer

Figma UI를 JSON으로 변환한 뒤, 해당 JSON을 WPF `Canvas` 위에 좌표 기반으로 렌더링하는 프로젝트입니다.

레이아웃은 트리 구조로 다시 계산하지 않고, JSON component의 `x`, `y`, `width`, `height` 값을 그대로 사용합니다.

## 렌더링 방식

- `canvas.width`, `canvas.height`, `canvas.background`로 기본 화면을 구성합니다.
- `components[]`를 순서대로 순회합니다.
- 각 component는 WPF `Canvas`에 absolute 좌표로 배치됩니다.
- `type`에 따라 전용 Renderer가 렌더링합니다.
- `bindings`가 있으면 mock 또는 서버 데이터 값을 사용해 치환합니다.

현재 지원하는 주요 component type:

- `rectangle`
- `text`
- `icon`
- `image`
- `horizontalLine`
- `blinkingText`

`type: "text"`이면서 `role: "ticketNumber"`인 경우에는 일반 text가 아니라 `TicketNumberComponentRenderer`를 사용합니다.

## appsettings.json

실행 설정은 `appsettings.json`에서 관리합니다.

```json
{
  "IsTest": true,
  "ServerIp": "192.168.150.234:7401",
  "DeviceCode": "DISP_2",
  "LayoutsFolder": "Layouts",
  "MockDevicesFile": "Mocks/devices.json"
}
```

### 설정 항목

`IsTest`
: `true`이면 로컬 mock 데이터를 사용합니다. `false`이면 서버 API를 호출합니다.

`ServerIp`
: 서버 API 기본 주소입니다. `http://`를 생략해도 됩니다.

`DeviceCode`
: 렌더링할 장비 코드입니다. 테스트 모드와 서버 모드 모두 이 값을 기준으로 동작합니다.

`LayoutsFolder`
: 테스트 모드에서 로컬 layout JSON을 찾을 폴더입니다.

`MockDevicesFile`
: 테스트 모드에서 사용할 mock device 목록 파일입니다.

## 테스트 모드

`IsTest`를 `true`로 설정하면 서버를 호출하지 않습니다.

```json
{
  "IsTest": true,
  "DeviceCode": "DISP_2",
  "LayoutsFolder": "Layouts",
  "MockDevicesFile": "Mocks/devices.json"
}
```

동작 순서:

1. `DeviceCode` 값으로 `Mocks/devices.json`에서 mock device를 찾습니다.
2. mock device의 `layout.code` 값을 읽습니다.
3. `Layouts/{layout.code}.json` 파일을 로드합니다.
4. 로드한 JSON을 화면에 렌더링합니다.

예시:

```json
{
  "code": "DISP_2",
  "layout": {
    "code": "QUEUE_DISPLAY_SCENE_DOUBLE"
  }
}
```

위 mock device는 아래 파일을 렌더링합니다.

```text
Layouts/QUEUE_DISPLAY_SCENE_DOUBLE.json
```

## 서버 모드

`IsTest`를 `false`로 설정하면 서버 API를 호출합니다.

```json
{
  "IsTest": false,
  "ServerIp": "192.168.150.234:7401",
  "DeviceCode": "DISP_2"
}
```

호출 URL:

```text
GET http://192.168.150.234:7401/api/Devices/DISP_2/layout
```

즉 Swagger의 아래 API와 연결됩니다.

```text
GET /api/Devices/{deviceCode}/layout
```

서버 응답은 렌더링 가능한 layout JSON이어야 합니다.

## 실행 방법

빌드:

```powershell
dotnet build
```

실행:

```powershell
dotnet run
```

Visual Studio에서 실행해도 됩니다.

## 파일 구성

```text
appsettings.json
Layouts/
Mocks/
Renderers/
LayoutDocumentLoader.cs
LayoutAppSettings.cs
FigmaLayoutRenderer.cs
FigmaLayoutModels.cs
```

주요 역할:

`LayoutAppSettings.cs`
: `appsettings.json`을 읽습니다.

`LayoutDocumentLoader.cs`
: 테스트 모드와 서버 모드에 따라 layout JSON을 가져옵니다.

`FigmaLayoutRenderer.cs`
: canvas 설정과 component 순회를 담당합니다.

`Renderers/`
: component type별 렌더링 클래스를 담고 있습니다.

`Layouts/`
: 테스트 모드에서 사용하는 layout JSON 파일을 둡니다.

`Mocks/devices.json`
: 테스트 모드에서 `DeviceCode`와 `layout.code`를 매핑합니다.
