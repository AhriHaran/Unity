﻿/*
 * 해야 하는 것들
 * 
 * <캐릭터 선택 패널>
 * 캐릭터 중복 선택 방지
 * 캐릭터 파티 탈퇴 구현
 * 선택된 캐릭터 구분하고 다른 패널에서 이를 선택 시 자리를 옮겨준다.
 * 선택 했다는 스프라이트 구현
 * 
 * <전체>
 * 전투 시작 -> 적 처치 -> 스테이지 종료 -> 메인 화면의 루틴을 돌려야 한다.
 * 스프라이트 개선
 * 로비 배경 구현
 * 
 * <전투>
 * 콤보 시스템 구현
 * int로 애니메이션 구분하여 실행
 * 
 */

/*
 * <스테이지 타입>
 * 노말 : 시작하면 바로 적이 나를 향해서 온다.
 * 잠입 : 들키지 말고 끝까지 가라, 이때는 플레이어와 일정 거리가 되어야 인식한다.
 * 보스 : 말그대로 보스
 */

/* 2019_08_14
 * JSON
 * 적이 내 공격을 맞으면 적의 HP 바를 보여준다.
 * 적이 죽으면 랙돌로 죽는 모션 체크
 * 조이스틱과 ui 버튼
 */

/*2019_08_16
 * 적이 내 공격을 맞으면 적의 HP 바를 보여준다.
* 적이 죽으면 랙돌로 죽는 모션 체크
* 조이스틱과 ui 버튼
 */