package com.gumigames.presentation.ui.monster

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.freeproject.happyprogrammers.base.BaseFragment
import com.gumigames.presentation.R
import com.gumigames.presentation.databinding.FragmentMonsterBinding
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class MonsterFragment : BaseFragment<FragmentMonsterBinding>(
    FragmentMonsterBinding::bind,
    R.layout.fragment_monster
) {

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        initListener()
    }
    override fun initListener() {
    }
}