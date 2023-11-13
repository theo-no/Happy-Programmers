package com.ggteam.single.api.account.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

import javax.persistence.Column;

@Getter
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class CharacterDto {

    private Long id;

    private String name;
    private char gender;
    private int exp;
    private int level;
    private int point;
    private int storyProgress;
    private String savepoint;
    private String imgPath;

}
