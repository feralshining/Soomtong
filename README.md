## 💨 숨통(Soomtong) 프로젝트 소개 💨

<img width="250" alt="Intro" src="https://raw.githubusercontent.com/feralshining/Soomtong/main/assets/숨통로고.png">  
<br>

### 💨 숨통이란?

> “텅 빈 기계 안에, 작지만 단단한 숨 하나.”

**숨통**은 낡고 방치된 키오스크 장비에 생명력을 불어넣는  **경량화 복지 키오스크 리뉴얼 솔루션 프로젝트**입니다.

단순 기술 구현을 넘어, **공공복지현장에 실질적인 도움**을 주는 것을 목표로 합니다.

<br>

### 📌 북구장애인종합복지관 적용 사례

 숨통 프로젝트의 1차 타겟 및 실제 적용 사례는
**부산광역시 북구장애인종합복지관** 이었습니다.
 
**북구장애인종합복지관**에는 수년간 사용되지 않은 채 방치된 키오스크가 존재했고,
 
이 장비에 직접 개발한 프로그램을 탑재하여,
 
 **관내 소식지 송출 시스템 및 QR 기반 출석 관리를** 현장에 맞게 구현하였습니다.
 
<br>

### 🎯 북구장복 적용 기획 배경

- ✅ **노후 키오스크 장비의 활용 부족**
- ✅ **수기 출석 및 안내의 반복적인 노동**
- ✅ **예산 없이 실질적으로 기능을 구현해야 함**
- ✅ **복지사 중심의 직관적인 관리 인터페이스 필요**

<br>

### 💡 북구장복 버전 핵심 기능

- `QR 코드 기반 대상자 출석 확인`
- `소식지 및 뉴스 안내문 이미지 송출`
- `출석 기록 엑셀 자동 저장`
- `복지사용 관리자 패널 (출석 현황, 메시지 등록 등)`
- `음성 안내 기능(TTS)`
- `버튼 최소화된 키오스크 UX`

<br>

### 🗓 개발 기간

- 개발 기간: 2025.05.12 ~ 2025.05.23 *(북구장복 1차 버전 완성)*
- 리팩토링 및 고도화: 2025.06 ~

<br>

### 🤝 협업 및 개발 도구

- 회의 및 문서: Notion, Discord
- 버전 관리: GitHub
- 개발 환경: Visual Studio 2022

<br>

### 💻 기술 스택 및 구조

| 분류 | 내용 |
| --- | --- |
| Language | C# |
| Framework | .NET Framework 4.7.2 |
| UI 방식 | Windows Forms (WinForms) |
| OS | Windows 10 이상 |
| Architecture | x64 |
| Build Tool | MSBuild + Costura.Fody |

<br>

### 📦 주요 라이브러리

| 기능 | 라이브러리 |
| --- | --- |
| 엑셀 처리 | `EPPlus`, `EPPlus.System.Drawing` |
| JSON 처리 | `Newtonsoft.Json` |
| UI 구성 | `MetroFramework`, `FontAwesome.Sharp` |
| 음성 안내 | `System.Speech` |
| 배포 최적화 | `Fody`, `Costura.Fody` |
| 시스템 연동 | `WindowsAPICodePack`, `System.Net.Http`, `System.Drawing`, `System.IO` 등 |

<br>


### 👨‍👩‍👧‍👦 사용자 흐름

- 대상자: 소식지 열람 + QR 코드를 가져와 키오스크 스캔 → 출석 완료
- 복지사: 관리자 패널을 통해 뉴스 이미지 교체 / 통계 확인 / 설정 변경

<br>

### ➡️ 서비스 흐름도
<img width="2000" alt="Intro" src="https://raw.githubusercontent.com/feralshining/Soomtong/main/assets/플로우차트.png">  
<br>

### 🖼 실제 구현 예시
<img width="250" alt="Intro" src="https://raw.githubusercontent.com/feralshining/Soomtong/main/assets/메인화면1.png">  
<br>

### 🌟 기대 효과

1. 복지사 업무 효율성 증가
2. 수기 출석 기록 → 자동화 및 통계화
3. 복지관 정보 전달력 향상
4. 낡은 장비의 재활용을 통한 예산 절감
5. 향후 다른 복지관으로도 확장 가능

<br>

### 🛠 다음 단계

- [ ]  타 복지기관 대상 기능 커스터마이징
- [ ]  소식지에서 맞춤 정보 제공 시스템으로 확장
- [ ]  오프라인 환경 대비 기능 강화
- [ ]  관리자 권한 별 접근 제어 기능 고도화
- [ ]  시빅뉴스 사례 제보 및 외부 홍보

<br>

### 🚀 실행 및 배포 환경

- Windows 10 이상 키오스크 단말기
- .NET Framework 4.7.2 설치
- RAM 2GB 이상 권장
- `.exe` 설치 파일 배포 (DLL 포함, Costura 통합)
