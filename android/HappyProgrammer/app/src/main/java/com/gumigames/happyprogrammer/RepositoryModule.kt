package com.gumigames.happyprogrammer

import com.gumigames.data.datasource.local.PreferenceDataSource
import com.gumigames.data.datasource.remote.GithubDataSource
import com.gumigames.data.repository.GithubRepositoryImpl
import com.gumigames.data.repository.LoginRepositoryImpl
import com.gumigames.domain.repository.GithubRepository
import com.gumigames.domain.repository.LoginRepository
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoryModule {
    @Singleton
    @Provides
    fun provideGithubRepository(githubDataSource: GithubDataSource): GithubRepository {
        return GithubRepositoryImpl(githubDataSource)
    }

    @Singleton
    @Provides
    fun provideLoginRepository(preferenceDataSource: PreferenceDataSource): LoginRepository {
        return LoginRepositoryImpl(preferenceDataSource)
    }

}