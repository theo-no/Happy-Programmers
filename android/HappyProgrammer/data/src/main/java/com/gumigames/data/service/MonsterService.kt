package com.gumigames.data.service

import com.gumigames.data.model.response.bookmark.BookmarkResponse
import com.gumigames.data.model.response.common.MonsterResponse
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface MonsterService {

    /**
     * 전체 몬스터 조회
     */
    @GET("guide/monsters")
    suspend fun getAllMonsters(): Response<List<MonsterResponse>>

    /**
     * 몬스터 검색
     */
    @GET("guide/monsters/search")
    suspend fun searchMonsters(@Query("keyword") keyword: String): Response<List<MonsterResponse>>
    /**
     * 몬스터 즐겨찾기 > 토글
     */
    @POST("guide/monsters/favorite/{monsterId}")
    suspend fun toggleBookmarkMonster(@Path("monsterId") monsterId: Int): Response<BookmarkResponse>
}