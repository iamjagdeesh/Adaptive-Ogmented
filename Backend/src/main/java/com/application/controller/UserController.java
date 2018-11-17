package com.application.controller;

import com.application.pojo.User;
import com.application.pojo.UserLogs;
import com.application.services.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/user")
public class UserController {

    @Autowired
    private UserService userService;

    @RequestMapping(value = "/test", method = RequestMethod.GET)
    public String test() {
        return "Test";
    }

    @RequestMapping(value = "/addUser", method = RequestMethod.POST)
    public User addUser(@RequestParam String userId,
                        @RequestParam String password,
                        @RequestParam Integer levelOfExpertise) {
        return userService.addUser(userId, password, levelOfExpertise);
    }

    @RequestMapping(value = "/{userId}", method = RequestMethod.GET)
    public User getUser(@PathVariable("userId") String userId) {
        return userService.getUser(userId);
    }

    @RequestMapping(value = "/checkIfValidCredentials", method = RequestMethod.GET)
    public Boolean checkIfValidCredentials(@RequestParam String userId,
                                           @RequestParam String password) {
        return userService.checkIfValidCredentials(userId, password);
    }

    @RequestMapping(value = "/logs/addUserLog", method = RequestMethod.POST)
    public UserLogs addUserLog(@RequestParam Integer speed,
                               @RequestParam Integer numberOfWallCollisions,
                               @RequestParam Integer taskNumber,
                               @RequestParam String userId,
                               @RequestParam Boolean isSuccess,
                               @RequestParam Long timeTaken) {
        return userService.addUserLog(speed, numberOfWallCollisions, taskNumber, userId, isSuccess, timeTaken);
    }

    @RequestMapping(value = "/logs/{userId}", method = RequestMethod.GET)
    public List<UserLogs> getUserLogs(@PathVariable("userId") String userId) {
        return userService.getUserLogs(userId);
    }

}
