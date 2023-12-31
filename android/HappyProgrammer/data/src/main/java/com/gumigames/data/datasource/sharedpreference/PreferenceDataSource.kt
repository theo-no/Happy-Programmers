package com.gumigames.data.datasource.sharedpreference

interface PreferenceDataSource {
    fun refreshPreference()
    fun getAccessToken(): String?
    fun setAccessToken(newToken: String)
    fun getRefreshToken(): String?
    fun setRefreshToken(newToken: String)
    fun resetToken()
    fun setPermissionRejected(key: String, value: Boolean)
    fun getPermissionRejected(key: String): Boolean
    fun setIsShowedPermissionDialog(key: String, value: Boolean)
    fun getIsShowedPermissionDialog(key: String): Boolean
    fun getIsAlreadyShowedDialog(): Boolean
    fun setIsAlreadyShowedDialog(value: Boolean)
    fun getIsLogined(): Boolean
    fun setIsLogined(value: Boolean)
}