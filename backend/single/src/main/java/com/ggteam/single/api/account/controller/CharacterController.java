package com.ggteam.single.api.account.controller;

import com.ggteam.single.api.account.dto.CharacterDto;
import com.ggteam.single.api.account.service.CharacterService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.security.Principal;

@Tag(name = "캐릭터 정보", description = "캐릭터 관련")
@RequiredArgsConstructor
@RestController
@RequestMapping(value = "/api")
public class CharacterController {

    private final CharacterService characterService;

    // 내 캐릭터 정보 가져오기
    @GetMapping("/character")
    @Operation(summary = "캐릭터 정보", description = "내 캐릭터 정보를 가져온다. 로그인 상태(토큰필요)에서 요청 시 정보를 줌")
    public ResponseEntity<?> myCharacter(Principal principal) {
        return characterService.myCharacter(principal.getName());
    }

    // 캐릭터 정보 저장
    @PostMapping("/character/save")
    @Operation(summary = "캐릭터 정보 저장", description = "JSON으로 이름(name, String), 성별(gender, char), 경험치(exp, int)," +
            " 레벨(level, int), 포인트(point, int), 저장위치(savepoint, int), 캐릭터 이미지(imgPath, String) 필요")
    public ResponseEntity<?> saveCharacter(@RequestBody CharacterDto characterDto) {
        return characterService.saveCharater(characterDto);
    }
}
