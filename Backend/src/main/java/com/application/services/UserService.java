package com.application.services;

import com.application.pojo.User;
import com.application.repositories.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    public User addUser(String userId, String password, Integer levelOfExpertise) {
        User user = new User(userId, password, levelOfExpertise);
        userRepository.save(user);

        return user;
    }

    public User getUser(String userId) {
        User user = userRepository.findByUserId(userId);

        return user;
    }

    public Boolean checkIfValidCredentials(String userId, String password) {
        User user = userRepository.findByUserIdAndPassword(userId, password);
        if (user == null) {
            return Boolean.FALSE;
        } else {
            return Boolean.TRUE;
        }
    }

}
