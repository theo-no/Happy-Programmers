package com.gumigames.data.service

import com.moneyminions.mvvmtemplate.dto.RepoResponse
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Path

interface GithubService {
    @GET("users/{user}/repos")
    suspend fun getUserRepos(@Path("user") user:String): Response<List<RepoResponse>>

}